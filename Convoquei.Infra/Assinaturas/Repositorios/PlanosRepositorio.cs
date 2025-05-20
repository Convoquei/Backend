using Convoquei.Core.Assinaturas.Entidades;
using Convoquei.Core.Assinaturas.Repositorios;
using Convoquei.Infra.Data;
using Convoquei.Infra.Genericos.Repositorios;

namespace Convoquei.Infra.Assinaturas.Repositorios
{
    public class PlanosRepositorio : RepositorioGenerico<Plano>, IPlanosRepositorio
    {
        public PlanosRepositorio(AppDbContext context) : base(context)
        {
        }
    }
}
