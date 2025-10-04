using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Application.DTO
{
    public class ResultResponse<T>
    {
        public bool Sucess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
