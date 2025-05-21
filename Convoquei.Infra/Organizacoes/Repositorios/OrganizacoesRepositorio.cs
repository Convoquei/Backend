using Convoquei.Core.Genericos.Repositorios.Consultas;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Repositorios;
using Convoquei.Infra.Data;
using Convoquei.Infra.Genericos.Extensoes;
using Convoquei.Infra.Genericos.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Convoquei.Infra.Organizacoes.Repositorios
{
    public class OrganizacoesRepositorio : RepositorioGenerico<Organizacao>, IOrganizacoesRepositorio
    {
        public OrganizacoesRepositorio(AppDbContext context) : base(context)
        {
        }

        public async Task<PaginacaoConsulta<ConviteOrganizacao>> ListarConvitesAsync(Guid idOrganizacao, int pagina, int tamanhoPagina, CancellationToken cancellationToken)
        {
            var query = _context.Set<ConviteOrganizacao>()
                .AsNoTracking()
                .Include(c => c.Convidador)
                .Where(c => c.Organizacao.Id == idOrganizacao)
                .OrderBy(c => c.DataCriacao);

            PaginacaoConsulta<ConviteOrganizacao> convites = await query.PaginarAsync(pagina, tamanhoPagina, cancellationToken);

            return convites;
        }
    }
}
