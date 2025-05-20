using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Application.Autenticacao
{
    public class UsuarioSessao : IUsuarioSessao
    {
        public Usuario Usuario { get; private set; } = null!;

        public void Definir(Usuario usuario)
            => Usuario = usuario;
    }
}
