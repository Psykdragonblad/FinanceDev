using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Application.DTO
{
    public record DI1CurvaRelatorioDto(double PuAjusteAtual, double FatorTaxaImplicita, double TaxaImplicita, int DiasCorridos, string codVencimento);
}
