using FinanceDev.Application.DTO;
using FinanceDev.Domain.Interface.Service;
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
        public string GetAction(int id) {
            return "teste";
        }

        [HttpPost("GerarCarga")]
        public string GerarCarga([FromBody] GerarCargaRequest request)
        {
            _curvaService.Add(request.Data);
            return "teste";
        }
    }
}
