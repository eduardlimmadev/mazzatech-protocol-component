using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Protocol.Publisher.Application.AppServices.Interfaces;
using Protocol.Publisher.Domain.Dtos;
using Protocol.Publisher.Shared.FlowControl.Enums;
using Protocol.Publisher.Shared.FlowControl.Models;
using Protocol.Shared.Domain.Dtos;

namespace Protocol.Publisher.Presentation.Controllers
{
    [ApiController]
    [EnableCors("ProtocolsCorsPolicy")]
    [Route("[controller]")]
    public class ProtocolPublisherController : ControllerBase
    {
        private readonly IPublisherAppService _publisherAppService;

        public ProtocolPublisherController(IPublisherAppService publisherAppService)
        {
            _publisherAppService = publisherAppService;
        }

        [HttpPost]
        public async Task<IActionResult> PublishMessageAsync([FromForm] PublishProtocolDto protocol)
        {
            try
            {
                var result = await _publisherAppService.PublishMessageAsync(protocol);

                if (result.HasError)
                    return ConvertToHttpError(result.Errors);

                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred {ex.Message}\n{ex.StackTrace}");
            }
        }

        private IActionResult ConvertToHttpError(IEnumerable<Error> errors)
        {
            var firstError = errors.First();
            var statusCode = ConvertToHttpStatusCode(firstError.Type);
            return StatusCode(statusCode, errors);
        }

        private int ConvertToHttpStatusCode(ErrorType type)
        {
            return type switch
            {
                ErrorType.Business => StatusCodes.Status400BadRequest,
                ErrorType.Unhandled => StatusCodes.Status500InternalServerError,
                ErrorType.FailedDependency => StatusCodes.Status424FailedDependency,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
