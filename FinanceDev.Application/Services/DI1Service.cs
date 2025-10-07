using ClosedXML.Excel;
using FinanceDev.Application.Helpers;
using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Domain.Interface.Service;
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
        private readonly string? _caminhoArquivo;

        public DI1Service(IDI1CurvaRepository dI1CurvaRepository, IReferenciaCurvaRepository ReferenciaCurvaRepository, IConfiguration configuration) 
        { 
            _dI1CurvaRepository = dI1CurvaRepository;
            _ReferenciaCurvaRepository = ReferenciaCurvaRepository;
            _caminhoArquivo = configuration["Arquivos:DI1Curva"];
        }

        public async Task<ResultResponse<IEnumerable<DI1Curva>>> GetByDataAsync(DateTime date)
        {
            try
            {
                var retorno = await _dI1CurvaRepository.GetByDataAsync(date);

                if (!retorno.Any())
                {
                    return ResultResponse<IEnumerable<DI1Curva>>.Fail("Dados não encontrado");
                }

                return ResultResponse<IEnumerable<DI1Curva>>.Ok(retorno);
            }
            catch (Exception e)
            {
                return ResultResponse<IEnumerable<DI1Curva>>.Fail($"Erro ao buscar dados: {e.Message}");
            }
        }

        public async Task<ResultResponse> Add(DateTime dataReferencia)
        {
            try
            {
                string caminhoArquivo = _caminhoArquivo + dataReferencia.ToString("dd-MM-yyyy") + ".xlsx";// @"C:\Users\Marcos\Desktop\DI1-03-10-2025.xlsx";
                var linhas = ExcelHelper.LerArquivo(caminhoArquivo, possuiCabecalho: false, linhaInicial: 18);
                List<DI1Curva> dd = new List<DI1Curva>();
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
                        /* dd.Add(new DI1Curva()
                         {
                             Vencimento = linha["Coluna1"],
                             Ajuste = double.Parse(linha["Coluna14"].Replace(".", ""), CultureInfo.InvariantCulture)//(double)linha["Coluna14"]
                         });*/
                        var di1curva = new DI1Curva
                        {
                            Vencimento = linha["Coluna1"],
                            Ajuste = double.Parse(linha["Coluna14"].Replace(".", ""), CultureInfo.InvariantCulture),
                            IdReferenciaCurva = referencia.Id
                        };

                        await _dI1CurvaRepository.AddAsync(di1curva);
                    }

                    Console.WriteLine(linha["Coluna1"]+" - "+ linha["Coluna14"]);

                    /* var item = new MesVencimento
                     {
                         Nome = linha.GetValueOrDefault("Nome"),
                         Data = DateTime.TryParse(linha.GetValueOrDefault("Data"), out var dt) ? dt : DateTime.MinValue,
                         Valor = decimal.TryParse(linha.GetValueOrDefault("Valor"), out var val) ? val : 0m
                     };

                     lista.Add(item);*/
                }
                //await _dI1CurvaRepository.AddAsync(new DI1Curva { });
                return ResultResponse.Ok("DI1 Cadastrado com sucesso!");
            }
            catch (Exception e)
            {

                return ResultResponse.Fail($"Erro ao gerar carga: {e.Message}");
            }
            
        }
        /* public Task<List<DI1Curva>> GetDI1Curva(DateTime dataReferencia) {
             if (dataReferencia == DateTime.MinValue)
                 return BadRequst();
         }*/
        public void teste()
        {
            string caminhoArquivo = @"C:\Users\Marcos\Desktop\DI1-03-10-2025.xlsx";
            var linhas = ExcelHelper.LerArquivo(caminhoArquivo, possuiCabecalho: false,linhaInicial:18);
            List<DI1Curva> dd = new List<DI1Curva>();
            foreach (var linha in linhas)
            {
                if(linha["Coluna14"] != "")
                {
                    dd.Add(new DI1Curva()
                    {
                        Vencimento = linha["Coluna1"],
                        Ajuste = double.Parse(linha["Coluna14"].Replace(".", ""), CultureInfo.InvariantCulture)//(double)linha["Coluna14"]
                    });
                }
               
                //Console.WriteLine(linha["Coluna1"]+" - "+ linha["Coluna14"]);
                
               /* var item = new MesVencimento
                {
                    Nome = linha.GetValueOrDefault("Nome"),
                    Data = DateTime.TryParse(linha.GetValueOrDefault("Data"), out var dt) ? dt : DateTime.MinValue,
                    Valor = decimal.TryParse(linha.GetValueOrDefault("Valor"), out var val) ? val : 0m
                };

                lista.Add(item);*/
            }
            foreach (var item in dd)
            {
                Console.WriteLine(item.Vencimento + " - " +item.Ajuste);
            }
            /* string caminhoArquivo = @"C:\Users\Marcos\Desktop\DI1-03-10-2025.xlsx";

             // Abre o arquivo
             using (var workbook = new XLWorkbook(caminhoArquivo))
             {
                 var planilha = workbook.Worksheet(1); // ou pelo nome: workbook.Worksheet("Planilha1")

                 // Identifica o intervalo usado na planilha
                 var range = planilha.RangeUsed();

                 // Percorre as linhas e colunas
                 foreach (var linha in range.Rows().Skip(16))
                 {
                     foreach (var celula in linha.Cells())
                     {
                         Console.Write(celula.GetValue<string>() + "\t");
                     }
                     Console.WriteLine();
                 }
             }*/

        }
    }
}
