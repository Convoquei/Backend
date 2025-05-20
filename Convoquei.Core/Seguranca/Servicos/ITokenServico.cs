using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.ValueObjects;

namespace Convoquei.Core.Seguranca.Servicos
{
    public interface ITokenServico
    {
        Token GerarToken(Usuario usuario);
    }
}
