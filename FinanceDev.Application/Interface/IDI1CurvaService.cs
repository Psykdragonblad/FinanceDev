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
        ResultResponse<IEnumerable<DI1CurvaDto>> GetByData(DateTime date);
        Task<ResultResponse> Add(DateTime dataReferencia);
        ResultResponse<IEnumerable<DI1CurvaRelatorioDto>> CurvaDI1(DateTime dataReferencia);
    }
}
