using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Application.Helpers
{
    public static class DataUtils
    {
        public static int DiasUteis(DateTime inicio, DateTime fim, List<DateTime>? feriados = null, bool excluirDiaInicio = false)
        {
            if (inicio > fim)
                throw new ArgumentException("A data de início não pode ser maior que a data final.");

            int totalDias = 0;
            feriados ??= new List<DateTime>();

            for (DateTime data = inicio; data <= fim; data = data.AddDays(1))
            {
                // Verifica se é sábado/domingo ou feriado
                if (data.DayOfWeek != DayOfWeek.Saturday &&
                    data.DayOfWeek != DayOfWeek.Sunday &&
                    !feriados.Contains(data.Date))
                {
                    totalDias++;
                }
            }

            if (excluirDiaInicio)
                totalDias = Math.Max(0, totalDias - 1);

            return totalDias;
        }
    }
}
