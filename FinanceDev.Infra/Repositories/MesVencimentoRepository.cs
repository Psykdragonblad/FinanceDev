using FinanceDev.Domain;
using FinanceDev.Domain.Interface;
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
    public class MesVencimentoRepository: IMesVencimentoRepository
    {
        private readonly AppDbContext _appDbContext;

        public MesVencimentoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<List<MesVencimento>> GetAll() {
            return _appDbContext.MesVencimento.ToListAsync();
        }
    }
}
