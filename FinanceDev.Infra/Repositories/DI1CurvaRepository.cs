using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace FinanceDev.Infra.Repositories
{
    public class DI1CurvaRepository :IDI1CurvaRepository
    {
        private readonly AppDbContext _context;
        public DI1CurvaRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task AddAsync(DI1Curva entidade)
        {
            await _context.AddAsync(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DI1Curva>> GetByDataAsync(DateTime data)
        {
            return await _context.DI1Curva.Where(e => e.ReferenciaCurva.DataReferencia == data).AsNoTracking().ToListAsync();

        }
    }
}
