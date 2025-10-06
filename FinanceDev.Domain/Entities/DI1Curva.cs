using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Entities
{
    public class DI1Curva
    {
        public string Vencimento { get; set; }
        public double Ajuste { get; set; }
        public int IdReferenciaCurva { get; set; }
        public ReferenciaCurva ReferenciaCurva { get; set; }
    }
}
