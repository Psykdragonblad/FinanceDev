using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Infraestructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Infra.Repositories
{
    public class FeriadoRepository : IFeriadosRepository
    {
        private readonly AppDbContext _appDbContext;
        public FeriadoRepository(AppDbContext appDbContext) { 
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Feriado>> GetAll()
        {
            return await _appDbContext.Feriado.ToListAsync();
        }
    }
}
