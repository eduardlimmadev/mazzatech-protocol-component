using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Protocol.FileService.Application.Services.Interfaces;
using Protocol.FileService.Presentation.RequestModels;

namespace Protocol.FileService.Presentation.Controllers
{
    [ApiController]
    [EnableCors("ProtocolsCorsPolicy")]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileAppService _fileAppService;

        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpPost("upload")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<IActionResult> Upload([FromForm] FileUploadRequestModel requestModel)
        {
            try
            {
                var fileId = await _fileAppService.UploadFileAsync(requestModel.File);
                return Ok(fileId.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred {ex.Message}\n{ex.StackTrace}");
            }
        }

        [HttpGet("download/{fileId}")]
        public async Task<IActionResult> Download(Guid fileId)
        {
            try
            {
                var fileDto = await _fileAppService.DownloadFileAsync(fileId);
                return Ok(fileDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
