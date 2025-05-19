using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Convoquei.Infra.Genericos.Repositorios
{
    public abstract class RepositorioGenerico<TEntidade> : IRepositorioGenerico<TEntidade> where TEntidade : EntidadeBase
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntidade> _dbSet;

        protected RepositorioGenerico(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntidade>();
        }
    }
}
