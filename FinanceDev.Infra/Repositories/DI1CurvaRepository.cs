using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Infra.Context;
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

        public async Task AddAsync(DI1Curva entidade, bool autoSave)
        {
            await _context.AddAsync(entidade);

            if(autoSave) 
                await _context.SaveChangesAsync();
        }

        public IQueryable<DI1Curva> GetByDataAsync(DateTime data)
        {
            return _context.DI1Curva.Where(e => e.ReferenciaCurva.DataReferencia == data).AsNoTracking();

        }
    }
}
