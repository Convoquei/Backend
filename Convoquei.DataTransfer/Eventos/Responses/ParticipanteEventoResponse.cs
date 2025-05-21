using Convoquei.Core.Eventos.Entidades;
using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.DataTransfer.Eventos.Responses;

public record ParticipanteEventoResponse(
    UsuarioResponse Usuario,
    EventoResponse? Evento
)
{
    public static explicit operator ParticipanteEventoResponse(ParticipanteEvento participanteEvento)
    {
        return new ParticipanteEventoResponse(
            (UsuarioResponse)participanteEvento.Usuario,
            (EventoResponse?)participanteEvento.Evento
        );
    }
}