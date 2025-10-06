using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Interface.Service
{
    public interface IDI1CurvaService
    {
        Task<ResultResponse<IEnumerable<DI1Curva>>> GetByDataAsync(DateTime date);
        Task<ResultResponse> Add(DateTime dataReferencia);
    }
}
