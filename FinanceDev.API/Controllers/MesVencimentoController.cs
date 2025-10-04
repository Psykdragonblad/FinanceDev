using FinanceDev.Application.DTO;
using FinanceDev.Application.Services;
using FinanceDev.Domain;
using FinanceDev.Domain.Interface;
using FinanceDev.Domain.Interface.Service;
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
        public ActionResult<ResultResponse<MesVencimento>> GetAll() {
           var result = _mesVencimentoService.GetAll();
            return Ok(result);
        }
    }
}
