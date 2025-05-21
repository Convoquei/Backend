using System.ComponentModel;

namespace Convoquei.Core.Eventos.Enumeradores
{
    public enum TipoEventoEnum
    {
        [Description("Evento nao recorrente")]
        DataUnica = 1,
        [Description("Evento recorrente semanal")]
        Semanal = 2,
        [Description("Evento recorrente por intervalo de dias")]
        IntervaloDias = 3
    }
}
