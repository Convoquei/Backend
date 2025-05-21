using Convoquei.Core.Genericos.Repositorios;
using Convoquei.Core.Genericos.Repositorios.Consultas;
using Convoquei.Core.Organizacoes.Entidades;

namespace Convoquei.Core.Organizacoes.Repositorios
{
    public interface IOrganizacoesRepositorio : IRepositorioGenerico<Organizacao>
    {
        Task<PaginacaoConsulta<ConviteOrganizacao>> ListarConvitesAsync(Guid idOrganizacao, int pagina, int tamanhoPagina, CancellationToken cancellationToken);
    }
}
