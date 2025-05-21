using System.Reflection;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Repositorios.Consultas;
using Convoquei.DataTransfer.Genericos;

namespace Convoquei.Application.Genericos;

public static class PaginacaoExtensoes
{
    public static PaginacaoResponse<TDestino> MapearPaginacaoResponse<TOrigem, TDestino>(this PaginacaoConsulta<TOrigem> consulta) 
        where TOrigem : EntidadeBase where TDestino : class
    {
        var metodo = typeof (TDestino).GetMethod("op_Explicit", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(TOrigem) }, null);
        
        if(metodo is null) 
            throw new InvalidOperationException($"Não foi possível encontrar o método de conversão explícita de {typeof(TOrigem).Name} para {typeof(TDestino).Name}.");
        
        return new PaginacaoResponse<TDestino>
        (
            consulta.Dados.Select(d => (TDestino)metodo.Invoke(null, new object[] { d })),
            consulta.Total,
            consulta.Pagina,
            consulta.TamanhoPagina
        );
    }
}