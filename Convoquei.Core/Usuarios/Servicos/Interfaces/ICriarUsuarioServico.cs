using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Usuarios.Servicos.Interfaces
{
    public interface ICriarUsuarioServico
    {
        Task<Usuario> CriarAsync(string nome, string email, string senha, CancellationToken cancellationToken);
    }
}
