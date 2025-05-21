using Convoquei.Core.Eventos.ValueObjects;
using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.DataTransfer.Eventos.Responses;

public record DadosCancelamentoEventoResponse(
    DateTime Data,
    string Motivo,
    UsuarioResponse Usuario
)
{
    public static explicit operator DadosCancelamentoEventoResponse(DadosCancelamentoEvento dadosCancelamento)
    {
        return new DadosCancelamentoEventoResponse(
            dadosCancelamento.Data,
            dadosCancelamento.Motivo,
            (UsuarioResponse)dadosCancelamento.Usuario
        );
    }
};