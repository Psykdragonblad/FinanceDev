using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Infraestructure.Repositories
{
    public class DI1Repository
    {
        private readonly AppDbContext _context;

        public DI1Repository(AppDbContext context)
        {
            _context = context;
        }

        public void AddAsync(DI1 entidade)
        {
           _context.DI1.AddAsync(entidade);
           _context.SaveChanges();
        }

        public async Task<IEnumerable<DI1>> GetByDataAsync(DateTime data) {
            return await _context.DI1.Where(e => e.DataReferencia == data).AsNoTracking().ToListAsync();
        
        }
      
    }
}
