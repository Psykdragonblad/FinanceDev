using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Infra.Repositories
{
    public class FeriadoRepository : IFeriadoRepository
    {
        private readonly AppDbContext _appDbContext;
        public FeriadoRepository(AppDbContext appDbContext) { 
            _appDbContext = appDbContext;
        }
        public IQueryable<Feriado> GetAll()
        {
            return _appDbContext.Feriado.AsNoTracking();
        }
    }
}
