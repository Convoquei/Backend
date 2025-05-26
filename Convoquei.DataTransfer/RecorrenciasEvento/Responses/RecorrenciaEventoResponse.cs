using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.RecorrenciasEvento.Entidades;

namespace Convoquei.DataTransfer.RecorrenciasEvento.Responses
{
    public record RecorrenciaEventoResponse(
        Guid Id,
        string Nome,
        string Descricao,
        string Local,
        string Recorrencia,
        TimeSpan? HoraInicio
    )
    {
        public static explicit operator RecorrenciaEventoResponse(RecorrenciaEventoBase recorrencia)
        {
            TimeSpan? horaInicio = null!;

            if (recorrencia is RecorrenciaEventoPeriodico)
                horaInicio = ((RecorrenciaEventoPeriodico)recorrencia).PrimeiraOcorrencia.TimeOfDay;
            else if (recorrencia is RecorrenciaEventoSemanal)
                horaInicio = ((RecorrenciaEventoSemanal)recorrencia).HorarioInicio;

            return new RecorrenciaEventoResponse(
                recorrencia.Id,
                recorrencia.Nome,
                recorrencia.Descricao,
                recorrencia.Local,
                recorrencia.DescricaoRecorrencia,
                horaInicio
            );
        }
    }
}
