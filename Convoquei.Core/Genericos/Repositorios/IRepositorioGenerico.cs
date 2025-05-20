using Convoquei.Core.Genericos.Entidades;
using System.Linq.Expressions;

namespace Convoquei.Core.Genericos.Repositorios
{
    public interface IRepositorioGenerico<T> where T : EntidadeBase
    {
        Task<T?> RecuperarAsync(Guid id, CancellationToken cancellationToken);
        Task<T?> RecuperarAsync(Expression<Func<T, bool>> expressao, CancellationToken cancellationToken);
        Task<IEnumerable<T>> ListarAsync(Expression<Func<T, bool>> expressao, CancellationToken cancellationToken);
        Task<T> InserirAsync(T entidade, CancellationToken cancellationToken);
        Task<T> EditarAsync(T entidade, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(T entidade, CancellationToken cancellationToken);
        IQueryable<T> Query();
    }
}
