using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Application.Autenticacao
{
    public interface IUsuarioSessao
    {
        Usuario Usuario { get; }
        void Definir(Usuario usuario);
    }
}
