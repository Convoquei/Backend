using System.Linq.Expressions;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Repositorios;
using Convoquei.Core.Genericos.Repositorios.Consultas;
using Microsoft.EntityFrameworkCore;

namespace Convoquei.Infra.Genericos.Repositorios
{
    public abstract class RepositorioGenerico<TEntidade> : IRepositorioGenerico<TEntidade>
        where TEntidade : EntidadeBase
    {
        protected readonly DbContext _context;
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

        public async Task<TEntidade?> RecuperarAsync(Expression<Func<TEntidade, bool>> expressao, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(expressao, cancellationToken);
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

        public IQueryable<TEntidade> Query()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntidade> QueryAsNoTracking()
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }

        public async Task<PaginacaoConsulta<TEntidade>> ListarAsync(IQueryable<TEntidade> query, int pagina, int tamanhoPagina, CancellationToken cancellationToken)
        {
            int totalRegistros = await query.CountAsync(cancellationToken);

            IEnumerable<TEntidade> registros = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync(cancellationToken);

            return new PaginacaoConsulta<TEntidade>(registros, totalRegistros, pagina, tamanhoPagina);
        }
    }
}
