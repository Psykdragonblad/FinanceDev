using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Entities
{
    public class MesVencimento
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Mes { get; set; }
    }
}
