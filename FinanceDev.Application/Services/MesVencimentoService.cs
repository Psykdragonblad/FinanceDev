using FinanceDev.Domain;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Application.Services
{
    public class MesVencimentoService : IMesVencimentoService
    {
        private readonly IMesVencimentoRepository _service;

        public MesVencimentoService(IMesVencimentoRepository service)
        {
            _service = service;
        }

        public async Task<List<MesVencimento>> GetAll() { 
            return await _service.GetAll();
        }
    }
}
