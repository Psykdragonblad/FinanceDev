using FinanceDev.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Interface.Shared
{
    public interface IGetAll<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}
