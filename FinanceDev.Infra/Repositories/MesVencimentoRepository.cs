using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Domain.Shared;
using FinanceDev.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Infra.Repositories
{
    public class MesVencimentoRepository: IMesVencimentoRepository
    {
        private readonly AppDbContext _appDbContext;

        public MesVencimentoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<MesVencimento>> GetAll() {
            return await _appDbContext.MesVencimento.AsNoTracking().ToListAsync();
        }
    }
}
