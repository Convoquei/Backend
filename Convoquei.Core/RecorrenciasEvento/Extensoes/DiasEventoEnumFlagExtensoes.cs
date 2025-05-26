using Convoquei.Core.RecorrenciasEvento.Enumeradores;

namespace Convoquei.Core.RecorrenciasEvento.Extensoes
{
    public static class DiasEventoEnumFlagExtensoes
    {
        public static IEnumerable<DayOfWeek> ObterDiasSemana(this DiasEventoEnumFlag dias)
        {
            foreach (DayOfWeek dia in Enum.GetValues(typeof(DayOfWeek)))
            {
                if (dias.HasFlag((DiasEventoEnumFlag)(1 << (int)dia)))
                {
                    yield return dia;
                }
            }
        }
    }
}
