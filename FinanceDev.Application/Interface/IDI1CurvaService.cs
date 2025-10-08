using FinanceDev.Application.DTO;
using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Application.Interface
{
    public interface IDI1CurvaService
    {
        Task<ResultResponse<IEnumerable<DI1CurvaDto>>> GetByDataAsync(DateTime date);
        Task<ResultResponse> Add(DateTime dataReferencia);
    }
}
