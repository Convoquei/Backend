using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.ValueObjects
{
    public record DadosCancelamentoEvento(
        DateTime Data,
        string Motivo,
        Usuario Usuario
    )
    {
        public static implicit operator string(DadosCancelamentoEvento dados)
        {
            return $"{dados.Data:dd/MM/yyyy} - {dados.Motivo} - {dados.Usuario.Nome}";
        }
    }
}