using ClosedXML.Excel;
using FinanceDev.Application.DTO;
using FinanceDev.Application.Helpers;
using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Application.Interface;
using FinanceDev.Domain.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Application.Services
{
    public class DI1Service: IDI1CurvaService
    {
        private readonly IDI1CurvaRepository _dI1CurvaRepository;
        private readonly IReferenciaCurvaRepository _ReferenciaCurvaRepository;
        private readonly IFeriadoRepository _feriadoRepository;
        private readonly string? _caminhoArquivo;

        public DI1Service(IDI1CurvaRepository dI1CurvaRepository, IReferenciaCurvaRepository ReferenciaCurvaRepository, IFeriadoRepository feriadoRepository,IConfiguration configuration) 
        { 
            _dI1CurvaRepository = dI1CurvaRepository;
            _ReferenciaCurvaRepository = ReferenciaCurvaRepository;
            _feriadoRepository = feriadoRepository;
            _caminhoArquivo = configuration["Arquivos:DI1Curva"];
        }

        public async Task<ResultResponse<IEnumerable<DI1CurvaDto>>> GetByDataAsync(DateTime date)
        {
            try
            {
                var retorno = await _dI1CurvaRepository.GetByDataAsync(date);

                if (!retorno.Any())
                {
                    return ResultResponse<IEnumerable<DI1CurvaDto>>.Fail("Dados não encontrado");
                }

                var retornoDto = retorno.Select(s => new DI1CurvaDto(
                                                s.Id,
                                                s.Vencimento,
                                                s.Ajuste
                                                )).ToList();

                return ResultResponse<IEnumerable<DI1CurvaDto>>.Ok(retornoDto);
            }
            catch (Exception e)
            {
                return ResultResponse<IEnumerable<DI1CurvaDto>>.Fail($"Erro ao buscar dados: {e.Message}");
            }
        }

        public async Task<ResultResponse> Add(DateTime dataReferencia)
        {
            try
            {
                string caminhoArquivo = Path.Combine(_caminhoArquivo, "DI1-" + dataReferencia.ToString("dd-MM-yyyy") + ".xlsx");
                var linhas = ExcelHelper.LerArquivo(caminhoArquivo, possuiCabecalho: false, linhaInicial: 18);

                if (linhas == null || !linhas.Any())
                    return ResultResponse.Fail("Nenhum dado encontrado no arquivo. Verifique se o formato e a linha inicial estão corretos.");


                if (!await _ReferenciaCurvaRepository.ExistsAsync(dataReferencia))
                {
                    var referencia = new ReferenciaCurva
                    {
                        Categoria = "DI1",
                        DataReferencia = dataReferencia
                    };

                    await _ReferenciaCurvaRepository.AddAsync(referencia);

                    foreach (var linha in linhas)
                    {
                        if (linha["Coluna14"] != "")
                        {
                            var di1curva = new DI1Curva
                            {
                                Vencimento = linha["Coluna1"],
                                Ajuste = double.Parse(linha["Coluna14"].Replace(".", ""), CultureInfo.InvariantCulture),
                                IdReferenciaCurva = referencia.Id
                            };

                            await _dI1CurvaRepository.AddAsync(di1curva);
                        }
                    }
                    return ResultResponse.Ok("DI1 Cadastrado com sucesso!");
                }
                else
                    return ResultResponse.Fail("Já existe uma carga para essa data de referência");
            }
            catch (Exception e)
            {
                return ResultResponse.Fail($"Erro ao gerar carga: {e.Message}");
            }            
        }

        public async Task<ResultResponse<IEnumerable<DI1Curva>>> CurvaDI1(DateTime dataReferencia)
        {
            double PuValorRef = 100000;

            var inicio = new DateTime(2025, 08, 15);
            var fim = new DateTime(2025, 11, 3);
            //List<DateTime> lista = new List<DateTime>();
            var feriados = await _feriadoRepository.GetAll();
            var datas = feriados.Select(r => r.Data);
            //lista.AddRange(datas);
            //lista.Add(new DateTime(2025,09,7));
            var curva = await _dI1CurvaRepository.GetByDataAsync(dataReferencia);
            //DI1CurvaRelatorioDto relatorio;

            // criar o vencimento
            foreach (var reg in curva) 
            {
                //var pu = DataUtils.DiasUteis(dataReferencia,reg.)
                var relatorio = new DI1CurvaRelatorioDto(
                    PuAjusteAtual: reg.Ajuste,
                    FatorTaxaImplicita: reg.Ajuste / PuValorRef,
                    TaxaImplicita: 1//,Math.Pow(reg.Ajuste / PuValorRef
                    );
            }

            var du = DataUtils.DiasUteis(inicio, fim, datas.ToList(), true);
            var dc = fim - inicio;
            var u = dc.Days;
            return ResultResponse<IEnumerable<DI1Curva>>.Ok(curva);
        }
        
    }
}
