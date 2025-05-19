using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.ValueObjects
{
    public class DadosCancelamentoEvento
    {
        public DateTime Data { get; init; }
        public string Motivo { get; init; }
        public Usuario Usuario { get; init; }

        public DadosCancelamentoEvento(DateTime data, string motivo, Usuario usuario)
        {
            Data = data;
            Motivo = motivo;
            Usuario = usuario;
        }

        private DadosCancelamentoEvento()
        {
            
        }

        public static implicit operator string(DadosCancelamentoEvento dados)
        {
            return $"{dados.Data:dd/MM/yyyy} - {dados.Motivo} - {dados.Usuario.Nome}";
        }
    }
}