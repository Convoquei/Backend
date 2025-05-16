using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.ValueObjects
{
    public record DelecaoEvento(
        Usuario Usuario,
        DateTime DataHoraDelecao,
        string MotivoDelecao
    );
}
