using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FinanceDev.Infra.Repositories
{
    public class MesVencimentoRepository: IMesVencimentoRepository
    {
        private readonly AppDbContext _appDbContext;

        public MesVencimentoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<MesVencimento> GetAll() {
            return _appDbContext.MesVencimento.AsNoTracking();
        }
    }
}
