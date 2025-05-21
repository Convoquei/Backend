using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.DataTransfer.Eventos.Responses;

public record DadosCancelamentoEventoResponse(
    DateTime Data,
    string Motivo,
    UsuarioResponse Usuario
);