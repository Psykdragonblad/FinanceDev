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
                    // Pega os cabecalhos da linha 1
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
                    // Gera nomes automaticos de coluna
                    var primeiraLinha = planilha.Row(linhaInicial);
                    var totalColunas = primeiraLinha.CellsUsed().Count();

                    for (int i = 1; i <= totalColunas; i++)
                    {
                        headers[i] = $"Coluna{i}";
                    }
                }

                // Le as linhas a partir da linha inicial
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

