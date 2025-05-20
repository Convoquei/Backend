using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Servicos.Interfaces
{
    public interface IOrganizacoesServico
    {
        Task<Organizacao> CriarAsync(string nome, Usuario criador, CancellationToken token);
    }
}
