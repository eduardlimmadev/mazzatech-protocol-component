using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Protocol.Application.Interfaces;
using Protocol.Shared.Domain.Dtos;

namespace Protocol.WebApi.Controllers
{
    [ApiController]
    [EnableCors("ProtocolsCorsPolicy")]
    [Route("[controller]")]
    public class ProtocolController : ControllerBase
    {
        private readonly IProtocolAppService _protocolAppService;

        public ProtocolController(IProtocolAppService protocolAppService)
        {
            _protocolAppService = protocolAppService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProtocolAsync([FromBody] ProtocolDto protocol)
        {
            try
            {
                var result = await _protocolAppService.CreateProtocolAsync(protocol);
                if (!result)
                    return BadRequest("Unable to save protocol");

                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred {ex.Message}\n{ex.StackTrace}");
            }
        }

        [HttpGet("number/{number}")]
        [Authorize]
        public async Task<IActionResult> GetProtocolByNumberAsync(int number)
        {
            var result = await _protocolAppService.GetProtocolByNumberAsync(number);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("list/rg/{rg}")]
        [Authorize]
        public async Task<IActionResult> GetAllProtocolsByRgAsync(string rg)
        {
            var result = await _protocolAppService.GetAllProtocolsByRgAsync(rg);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("list/cpf/{cpf}")]
        [Authorize]
        public async Task<IActionResult> GetAllProtocolsByCpfAsync(string cpf)
        {
            var result = await _protocolAppService.GetAllProtocolsByCpfAsync(cpf);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
