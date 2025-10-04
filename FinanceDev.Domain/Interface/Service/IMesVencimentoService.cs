using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Interface.Service
{
    public interface IMesVencimentoService
    {
        Task<List<MesVencimento>> GetAll();
    }
}
