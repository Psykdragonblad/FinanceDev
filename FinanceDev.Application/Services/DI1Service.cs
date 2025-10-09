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
        private readonly IMesVencimentoRepository _mesVencimentoRepository;
        private readonly string? _caminhoArquivo;

        public DI1Service(IDI1CurvaRepository dI1CurvaRepository, 
                         IReferenciaCurvaRepository ReferenciaCurvaRepository, 
                         IFeriadoRepository feriadoRepository,
                         IMesVencimentoRepository mesVencimentoRepository,
                         IConfiguration configuration) 
        { 
            _dI1CurvaRepository = dI1CurvaRepository;
            _ReferenciaCurvaRepository = ReferenciaCurvaRepository;
            _feriadoRepository = feriadoRepository;
            _mesVencimentoRepository = mesVencimentoRepository;
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
                return ResultResponse.Fail($"Erro ao gerar carga");
            }            
        }

        public async Task<ResultResponse<IEnumerable<DI1CurvaRelatorioDto>>> CurvaDI1(DateTime dataReferencia)
        {
            const double PuValorRef = 100000;

            try
            {
                var inicio = dataReferencia;
                var feriados = await _feriadoRepository.GetAll();
                var datas = feriados.Select(r => r.Data);

                var curva = await _dI1CurvaRepository.GetByDataAsync(dataReferencia);
                var mesVencimento = await _mesVencimentoRepository.GetAll();

                List<DI1CurvaRelatorioDto> curvas = new List<DI1CurvaRelatorioDto>();

                foreach (var reg in curva)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(reg.Vencimento) || reg.Vencimento.Length < 2)
                            throw new Exception($"Campo 'Vencimento' inválido: {reg.Vencimento}");

                        string mesCodigo = reg.Vencimento[0].ToString();
                        string anoCodigo = reg.Vencimento.Substring(1).ToString();

                        var mesVenciAux = mesVencimento.Where(e => e.Codigo == mesCodigo).FirstOrDefault();
                        if (mesVenciAux == null)
                            throw new Exception($"Mês não encontrado para código '{mesCodigo}'.");

                        int anoInt = int.Parse("20" + anoCodigo);
                        DateTime data = new DateTime(anoInt, mesVenciAux.Id, 1);
                        data = data.AddDays(-1);

                        var proximoDiaUtil = DataUtils.ProximoDiaUtil(data, datas.ToList());
                        var du = DataUtils.DiasUteis(inicio, proximoDiaUtil, datas.ToList(), true);
                        int dc = (proximoDiaUtil - inicio).Days;

                        double anualizador = (double)252 / (double)du;
                        double ajuste = Math.Round(reg.Ajuste,2) / 100;
                        double fatorTaxaImplicita = PuValorRef / ajuste;
                        double taxaImplicita = ((Math.Pow(PuValorRef / ajuste, anualizador) - 1) * 100);

                        var relatorio = new DI1CurvaRelatorioDto(
                            PuAjusteAtual: ajuste,
                            FatorTaxaImplicita: fatorTaxaImplicita,
                            TaxaImplicita: Math.Round(taxaImplicita, 2),
                            DiasCorridos: dc,
                            codVencimento: reg.Vencimento
                            );

                        curvas.Add(relatorio);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro ao processar curva {reg?.Vencimento}: {ex.Message}");
                    }
                }

                return ResultResponse<IEnumerable<DI1CurvaRelatorioDto>>.Ok(curvas);
            }
            catch (Exception ex)
            {
                return ResultResponse<IEnumerable<DI1CurvaRelatorioDto>>.Fail("Erro inesperado ao gerar a curva DI1.");
            }

        }     

    }
}
