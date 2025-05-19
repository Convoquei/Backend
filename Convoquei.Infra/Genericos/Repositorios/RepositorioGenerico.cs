using System.Linq.Expressions;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Convoquei.Infra.Genericos.Repositorios
{
    public abstract class RepositorioGenerico<TEntidade> : IRepositorioGenerico<TEntidade>
        where TEntidade : EntidadeBase
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntidade> _dbSet;

        protected RepositorioGenerico(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntidade>();
        }

        public async Task<TEntidade?> RecuperarAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TEntidade>> ListarAsync(Expression<Func<TEntidade, bool>> expressao, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(expressao).ToListAsync(cancellationToken);
        }

        public async Task<TEntidade> InserirAsync(TEntidade entidade, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entidade, cancellationToken);
            return entidade;
        }

        public Task<TEntidade> EditarAsync(TEntidade entidade, CancellationToken cancellationToken)
        {
            _dbSet.Update(entidade);
            return Task.FromResult(entidade);
        }

        public Task<bool> RemoverAsync(TEntidade entidade, CancellationToken cancellationToken)
        {
            _dbSet.Remove(entidade);
            return Task.FromResult(true);
        }
    }
}
