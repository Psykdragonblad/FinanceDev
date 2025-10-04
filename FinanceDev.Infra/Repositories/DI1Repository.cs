using FinanceDev.Domain;
using FinanceDev.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Infraestructure.Repositories
{
    public class DI1Repository : IAdicionar<DI1>
    {
        private readonly AppDbContext _context;

        public DI1Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Adicionar(DI1 entidade)
        {
           _context.DI1.Add(entidade);
           _context.SaveChanges();
        }
    }
}
