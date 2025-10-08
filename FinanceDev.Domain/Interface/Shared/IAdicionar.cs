using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Interface.Shared
{
    public interface IAdicionar<T>
    {
        Task AddAsync(T entidade);
    }
}
