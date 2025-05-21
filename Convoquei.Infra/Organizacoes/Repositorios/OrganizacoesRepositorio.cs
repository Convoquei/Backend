using Convoquei.Core.Genericos.Repositorios.Consultas;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Repositorios;
using Convoquei.Core.Usuarios.Entidades;
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

        public async Task<PaginacaoConsulta<Organizacao>> ListarOrganizacoesUsuarioAsync(Usuario usuario, int pagina,
            int tamanhoPagina, CancellationToken cancellationToken)
        {
            var query = _context.Set<MembroOrganizacao>()
                .AsNoTracking()
                .Include(c => c.Organizacao)
                .ThenInclude(o => o.Assinatura)
                .ThenInclude(a => a.Plano)
                .Include(o => o.Organizacao)
                .ThenInclude(o => o.Membros)
                .ThenInclude(m => m.Usuario)
                .Where(c => c.Usuario == usuario)
                .Select(c => c.Organizacao);

            PaginacaoConsulta<Organizacao> organizacoes =
                await query.PaginarAsync(pagina, tamanhoPagina, cancellationToken);

            return organizacoes;
        }
    }
}
