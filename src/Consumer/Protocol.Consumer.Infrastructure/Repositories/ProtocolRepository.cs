using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Protocol.Consumer.Domain.Options;
using Protocol.Consumer.Infrastructure.Polly.Interfaces;
using Protocol.Consumer.Infrastructure.Repositories.Interfaces;
using Protocol.Shared.Domain.Dtos;
using System.Text;
using System.Text.Json;

namespace Protocol.Consumer.Infrastructure.Repositories
{
    public class ProtocolRepository : IProtocolRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpClientPolicy _httpClientPolicy;
        private readonly ILogger _logger;
        private readonly Hosts _host;

        public ProtocolRepository(IHttpClientFactory httpClientFactory, IHttpClientPolicy httpClientPolicy, ILogger<ProtocolRepository> logger, IOptions<Hosts> host)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientPolicy = httpClientPolicy;
            _logger = logger;
            _host = GetHosts(host);
        }

        private Hosts GetHosts(IOptions<Hosts> hosts)
        {
            var protocolApi = Environment.GetEnvironmentVariable("PROTOCOL_API");

            if (!string.IsNullOrEmpty(protocolApi))
                return new Hosts { Protocol = protocolApi };

            return hosts?.Value ?? throw new ArgumentNullException(nameof(hosts));
        }

        public async Task<bool> SendProtocol(ProtocolDto protocol)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProtocolClient");

                var requestData = JsonSerializer.Serialize(protocol, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                var response = await _httpClientPolicy.LinearHttpRetry.ExecuteAsync(() =>
                    client.PostAsync(_host.Protocol, new StringContent(requestData, Encoding.UTF8, "application/json")));

                var errorList = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;

                _logger.LogInformation("ProtocolRepository.SendProtocol - StatusCode: {code} | Error List: {errors} | Protocol: {protocol}", response.StatusCode, errorList, requestData);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProtocolRepository.SendProtocol Error");
                return false;
            }
        }
    }
}
