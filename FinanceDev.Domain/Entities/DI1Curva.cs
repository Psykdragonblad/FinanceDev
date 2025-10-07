using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Entities
{
    public class DI1Curva
    {
        public int Id { get; set; }
        public string Vencimento { get; set; }
        public double Ajuste { get; set; }

        [ForeignKey("ReferenciaCurva")]
        public int IdReferenciaCurva { get; set; }
        public ReferenciaCurva ReferenciaCurva { get; set; }
    }
}
