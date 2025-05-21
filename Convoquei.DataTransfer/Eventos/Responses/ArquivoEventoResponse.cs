using Convoquei.Core.Eventos.Entidades;
using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.DataTransfer.Eventos.Responses;

public record ArquivoEventoResponse(
    EventoResponse Evento,
    string Nome,
    string MimeType,
    long TamanhoEmBytes,
    string ChaveStorage,
    UsuarioResponse Criador
)
{
    public static explicit operator ArquivoEventoResponse(ArquivoEvento arquivoEvento)
    {
        return new ArquivoEventoResponse(
            (EventoResponse)arquivoEvento.Evento,
            arquivoEvento.Nome,
            arquivoEvento.MimeType,
            arquivoEvento.TamanhoEmBytes,
            arquivoEvento.ChaveStorage,
            (UsuarioResponse)arquivoEvento.Criador
        );
    }
};