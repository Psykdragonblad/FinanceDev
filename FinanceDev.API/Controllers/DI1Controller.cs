using FinanceDev.Application.DTO;
using FinanceDev.Application.Interface;
using FinanceDev.Domain.Interface.Service;
using FinanceDev.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DI1Controller : ControllerBase
    {
        private readonly IDI1CurvaService _curvaService;

        public DI1Controller(IDI1CurvaService curvaService)
        { 
            _curvaService = curvaService;
        }

        [HttpGet("GetByData")]
        public async Task<ActionResult<IEnumerable<ResultResponse>>> GetAction([FromBody] GerarCargaRequest request) 
        {
            var retorno = await _curvaService.GetByDataAsync(request.Data);

            if (!retorno.Success)
                return BadRequest(retorno);

            return Ok(retorno);
        }

        [HttpPost("GerarCarga")]
        public async Task<ActionResult<ResultResponse>> GerarCarga([FromBody] GerarCargaRequest request)
        {
            var retorno = await _curvaService.Add(request.Data);

            if (!retorno.Success)
                return BadRequest(retorno);

            return Ok(retorno);
        }
    }
}
