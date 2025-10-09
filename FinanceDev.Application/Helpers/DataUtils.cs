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
                // Verifica se e sabado, domingo ou feriado
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

        public static DateTime ProximoDiaUtil(DateTime data, List<DateTime> feriados)
        {
            DateTime proximo = data.AddDays(1);

            // Loop ate achar um dia util
            while (EhFinalDeSemana(proximo) || EhFeriado(proximo, feriados))
            {
                proximo = proximo.AddDays(1);
            }

            return proximo;
        }

        private static bool EhFinalDeSemana(DateTime data)
        {
            return data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday;
        }

        private static bool EhFeriado(DateTime data, List<DateTime> feriados)
        {
            // Considera apenas a parte da data (sem hora)
            return feriados.Exists(f => f.Date == data.Date);
        }
    }
}
