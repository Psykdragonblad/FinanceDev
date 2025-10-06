using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Application.Helpers
{
    public static class ExcelHelper
    {
        /* public static List<T> LerPlanilha<T>(string caminhoArquivo, int linhaInicial = 2) where T : new()
         {
             var resultado = new List<T>();

             using (var workbook = new XLWorkbook(caminhoArquivo))
             {
                 var planilha = workbook.Worksheets.First();
                 var propriedades = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                 // Lê os cabeçalhos da primeira linha
                 var headers = new Dictionary<int, string>();
                 var primeiraLinha = planilha.Row(1);

                 foreach (var celula in primeiraLinha.CellsUsed())
                 {
                     headers[celula.Address.ColumnNumber] = celula.GetString().Trim();
                 }

                 // Lê as linhas de dados
                 foreach (var linha in planilha.RowsUsed().Where(r => r.RowNumber() >= linhaInicial))
                 {
                     var objeto = new T();

                     foreach (var prop in propriedades)
                     {
                         // Acha a coluna correspondente ao nome da propriedade
                         var coluna = headers.FirstOrDefault(h =>
                             string.Equals(h.Value, prop.Name, StringComparison.OrdinalIgnoreCase)).Key;

                         if (coluna > 0)
                         {
                             var celula = linha.Cell(coluna);
                             var valorCelula = celula.Value;

                             try
                             {
                                 if (!celula.IsEmpty() && !string.IsNullOrWhiteSpace(celula.GetString()))
                                 {
                                     var tipo = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                     var valorConvertido = Convert.ChangeType(valorCelula, tipo);
                                     prop.SetValue(objeto, valorConvertido);
                                 }
                             }
                             catch
                             {
                                 // Ignora erro de conversão individual
                             }
                         }
                     }

                     resultado.Add(objeto);
                 }
             }

             return resultado;
         }*/
        /// <summary>
        /// Lê um arquivo Excel e retorna as linhas como listas de dicionários.
        /// Cada item da lista representa uma linha, com o cabeçalho como chave.
        /// </summary>
        /*public static List<Dictionary<string, string>> LerArquivo(string caminhoArquivo, int linhaInicial = 2)
        {
            var resultado = new List<Dictionary<string, string>>();

            using (var workbook = new XLWorkbook(caminhoArquivo))
            {
                var planilha = workbook.Worksheets.First();
                var headers = new Dictionary<int, string>();

                // Cabeçalhos na linha 1
                foreach (var celula in planilha.Row(1).CellsUsed())
                {
                    headers[celula.Address.ColumnNumber] = celula.GetString().Trim();
                }

                // Linhas de dados
                foreach (var linha in planilha.RowsUsed().Where(r => r.RowNumber() >= linhaInicial))
                {
                    if (linha.IsEmpty()) continue;

                    var linhaDict = new Dictionary<string, string>();

                    foreach (var header in headers)
                    {
                        var celula = linha.Cell(header.Key);
                        var valor = celula.GetString().Trim();
                        linhaDict[header.Value] = valor;
                    }

                    resultado.Add(linhaDict);
                }
            }

            return resultado;
        }*/
        public static List<Dictionary<string, string>> LerArquivo(
           string caminhoArquivo,
           bool possuiCabecalho = true,
           int linhaInicial = 2)
        {
            var resultado = new List<Dictionary<string, string>>();

            using (var workbook = new XLWorkbook(caminhoArquivo))
            {
                var planilha = workbook.Worksheets.First();
                var headers = new Dictionary<int, string>();

                if (possuiCabecalho)
                {
                    // Pega os cabeçalhos da linha 1
                    foreach (var celula in planilha.Row(1).CellsUsed())
                    {
                        var nome = celula.GetString().Trim();
                        if (string.IsNullOrWhiteSpace(nome))
                            nome = $"Coluna{celula.Address.ColumnNumber}";
                        headers[celula.Address.ColumnNumber] = nome;
                    }
                }
                else
                {
                    // Gera nomes automáticos de coluna
                    var primeiraLinha = planilha.Row(linhaInicial);
                    var totalColunas = primeiraLinha.CellsUsed().Count();

                    for (int i = 1; i <= totalColunas; i++)
                    {
                        headers[i] = $"Coluna{i}";
                    }

                    // Se não há cabeçalho, começa da primeira linha
                    //linhaInicial = 1;
                }

                // Lê as linhas a partir da linha inicial
                foreach (var linha in planilha.RowsUsed().Where(r => r.RowNumber() >= linhaInicial))
                {
                    if (linha.IsEmpty()) continue;

                    var linhaDict = new Dictionary<string, string>();

                    foreach (var header in headers)
                    {
                        var celula = linha.Cell(header.Key);
                        var valor = celula.GetString().Trim();
                        linhaDict[header.Value] = valor;
                    }

                    resultado.Add(linhaDict);
                }
            }

            return resultado;
        }
    }
}

