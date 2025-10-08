using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Entities
{
    public class Feriado
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string DiaSemana { get; set; }
        public string Nome { get; set; }
    }
}
