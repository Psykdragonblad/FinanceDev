using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Interface.Repository
{
    public interface IDI1CurvaRepository: IAdicionar<DI1Curva>
    {
        IQueryable<DI1Curva> GetByDataAsync(DateTime data);
    }
}
