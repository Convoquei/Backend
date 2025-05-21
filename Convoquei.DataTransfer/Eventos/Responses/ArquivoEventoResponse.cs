using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.DataTransfer.Eventos.Responses;

public record ArquivoEventoResponse(
    EventoResponse Evento,
    string Nome,
    string MimeType,
    long TamanhoEmBytes,
    string ChaveStorage,
    UsuarioResponse Criador
);