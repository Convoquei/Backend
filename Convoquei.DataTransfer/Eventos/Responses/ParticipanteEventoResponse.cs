using Convoquei.Core.Eventos.Entidades;
using Convoquei.DataTransfer.Comuns.Responses;
using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.DataTransfer.Eventos.Responses;

public record ParticipanteEventoResponse(
    UsuarioResponse Usuario,
    GenericoEnumResponse<int> Status
)
{
    public static explicit operator ParticipanteEventoResponse(ParticipanteEvento participanteEvento)
    {
        return new ParticipanteEventoResponse(
            (UsuarioResponse)participanteEvento.Usuario,
            participanteEvento.StatusParticipacao
        );
    }
}