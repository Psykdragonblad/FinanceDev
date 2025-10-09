using FinanceDev.Application.Services;
using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface;
using FinanceDev.Domain.Interface.Service;
using FinanceDev.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDev.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MesVencimentoController : ControllerBase
    {
        private readonly IMesVencimentoService _mesVencimentoService;
        public MesVencimentoController(IMesVencimentoService mesVencimentoService)
        {
            _mesVencimentoService = mesVencimentoService;
        }

        [HttpGet]
        public ActionResult<ResultResponse<IEnumerable<MesVencimento>>> GetAll() {
           var result = _mesVencimentoService.GetAll();

            return Ok(result);
        }
    }
}
