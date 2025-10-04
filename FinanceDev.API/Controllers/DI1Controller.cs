using Microsoft.AspNetCore.Mvc;

namespace FinanceDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DI1Controller
    {
        [HttpGet("GetByData")]
        public string GetAction(int id) {
            return "teste";
        }

        [HttpPost("GerarCarga")]
        public string GetAction(DateTime data)
        {
            return "teste";
        }
    }
}
