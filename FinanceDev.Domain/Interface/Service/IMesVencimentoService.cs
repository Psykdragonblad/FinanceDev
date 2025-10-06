using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Shared;

namespace FinanceDev.Domain.Interface.Service
{
    public interface IMesVencimentoService
    {
        Task<ResultResponse<IEnumerable<MesVencimento>>> GetAll();
    }
}
