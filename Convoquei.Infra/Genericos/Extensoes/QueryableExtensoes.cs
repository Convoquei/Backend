using Convoquei.Core.Genericos.Repositorios.Consultas;
using Microsoft.EntityFrameworkCore;

namespace Convoquei.Infra.Genericos.Extensoes
{
    public static class QueryableExtensoes
    {
        public static async Task<PaginacaoConsulta<T>> PaginarAsync<T>(
            this IQueryable<T> query,
            int pagina,
            int tamanhoPagina,
            CancellationToken cancellationToken)
        {
            int totalRegistros = await query.CountAsync(cancellationToken);

            var registros = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync(cancellationToken);

            return new PaginacaoConsulta<T>(registros, totalRegistros, pagina, registros.Count);
        }
    }
}
