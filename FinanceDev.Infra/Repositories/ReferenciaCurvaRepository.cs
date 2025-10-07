using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Infraestructure;

namespace FinanceDev.Infra.Repositories
{
    public class ReferenciaCurvaRepository : IReferenciaCurvaRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReferenciaCurvaRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(ReferenciaCurva entidade)
        {
            await _appDbContext.AddAsync(entidade);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
