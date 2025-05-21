using Convoquei.Core.Recorrencias.Entidades;

namespace Convoquei.DataTransfer.RecorrenciasEvento.Responses
{
    public record RecorrenciaEventoResponse(
        Guid Id,
        string Nome,
        string Descricao,
        string Local,
        string Recorrencia,
        TimeSpan HoraInicio
    )
    {
        public static explicit operator RecorrenciaEventoResponse(RecorrenciaEventoBase recorrencia)
        {
            return new RecorrenciaEventoResponse(
                recorrencia.Id,
                recorrencia.Nome,
                recorrencia.Descricao,
                recorrencia.Local,
                recorrencia.DescricaoRecorrencia,
                recorrencia.DataHoraInicio.TimeOfDay
            );
        }
    }
}
