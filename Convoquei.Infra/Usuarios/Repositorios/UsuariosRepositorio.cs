using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.Repositorios;
using Convoquei.Infra.Data;
using Convoquei.Infra.Genericos.Repositorios;

namespace Convoquei.Infra.Usuarios.Repositorios
{
    public class UsuariosRepositorio : RepositorioGenerico<Usuario>, IUsuariosRepositorio
    {
        public UsuariosRepositorio(AppDbContext context) : base(context)
        {
        }
    }
}
