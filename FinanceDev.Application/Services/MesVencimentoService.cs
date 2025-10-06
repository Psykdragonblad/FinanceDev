using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Domain.Interface.Service;
using FinanceDev.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Application.Services
{
    public class MesVencimentoService : IMesVencimentoService
    {
        private readonly IMesVencimentoRepository _repository;

        public MesVencimentoService(IMesVencimentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultResponse<IEnumerable<MesVencimento>>> GetAll() 
        {
            try
            {
                var mesVencimentos = await _repository.GetAll();

                if (!mesVencimentos.Any())
                    return ResultResponse<IEnumerable<MesVencimento>>.Fail("Não foi encontrado a listagem de vencimentos");

                return ResultResponse<IEnumerable<MesVencimento>>.Ok(mesVencimentos);
            }
            catch (Exception e)
            {
                return ResultResponse<IEnumerable<MesVencimento>>.Fail($"Erro ao buscar a listagem vencimentos: { e.Message}");
            }
           
        }
    }
}
