using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Protocol.Publisher.Domain.Options;
using Protocol.Publisher.Infrastructure.Polly.Interfaces;
using Protocol.Publisher.Service.Services.Interfaces;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Protocol.Publisher.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpClientPolicy _httpClientPolicy;
        private readonly ILogger _logger;
        private readonly Hosts _host;

        public FileRepository(IHttpClientFactory httpClientFactory, IHttpClientPolicy httpClientPolicy, ILogger<FileRepository> logger, IOptions<Hosts> host)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientPolicy = httpClientPolicy;
            _logger = logger;
            _host = Gethosts(host);
        }

        private Hosts Gethosts(IOptions<Hosts> hosts)
        {
            var fileServiceAPI = Environment.GetEnvironmentVariable("FILE_SERVICE_API");

            if (!string.IsNullOrEmpty(fileServiceAPI))
                return new Hosts { FileService = fileServiceAPI };

            return hosts?.Value ?? throw new ArgumentNullException(nameof(hosts));
        }

        public async Task<Guid?> UploadFile(IFormFile file)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("FileServiceClient");

                using (var form = new MultipartFormDataContent())
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var streamContent = new StreamContent(stream);
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

                        form.Add(streamContent, "File", file.FileName);

                        var response = await _httpClientPolicy.LinearHttpRetry.ExecuteAsync(() =>
                            client.PostAsync($"{_host.FileService}/File/upload", form));

                        var responseData = await response.Content.ReadAsStringAsync() ?? string.Empty;

                        if (!response.IsSuccessStatusCode)
                        {
                            _logger.LogInformation("FileRepository.UploadFile - StatusCode: {code} | Error List: {errors} | File: {file}", response.StatusCode, responseData, file.FileName);
                            return null;
                        }

                        _logger.LogInformation("FileRepository.UploadFile - StatusCode: {code} | ResponseData: {errors} | File: {file}", response.StatusCode, responseData, file.FileName);
                        return Guid.Parse(responseData.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FileRepository.UploadFile Error");
                return null;
            }
        }
    }
}
