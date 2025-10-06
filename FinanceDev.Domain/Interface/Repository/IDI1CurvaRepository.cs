using FinanceDev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Interface.Repository
{
    public interface IDI1CurvaRepository: IAdicionar<DI1Curva>
    {
        Task<IEnumerable<DI1Curva>> GetByDataAsync(DateTime data);
    }
}
