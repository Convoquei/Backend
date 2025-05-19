using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Repositorios;
using Convoquei.Infra.Data;
using Convoquei.Infra.Genericos.Repositorios;

namespace Convoquei.Infra.Organizacoes.Repositorios
{
    public class OrganizacoesRepositorio : RepositorioGenerico<Organizacao>, IOrganizacoesRepositorio
    {
        public OrganizacoesRepositorio(AppDbContext context) : base(context)
        {
        }
    }
}
