using System.Reflection;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Repositorios.Consultas;
using Convoquei.DataTransfer.Genericos.Responses;

namespace Convoquei.Application.Genericos;

public static class PaginacaoExtensoes
{
    public static PaginacaoResponse<TDestino> MapearPaginacaoResponse<TOrigem, TDestino>(
    this PaginacaoConsulta<TOrigem> consulta)
    where TOrigem : EntidadeBase
    where TDestino : class
    {
        var tipoOrigem = typeof(TOrigem);
        if (tipoOrigem.Namespace?.StartsWith("Castle.Proxies") == true)
            tipoOrigem = tipoOrigem.BaseType ?? tipoOrigem;

        var metodo = typeof(TDestino).GetMethod(
            "op_Explicit",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new[] { tipoOrigem },
            null);

        if (metodo is null)
            throw new InvalidOperationException(
                $"Não foi possível encontrar o operador explicit de {tipoOrigem.Name} para {typeof(TDestino).Name}.");

        var dadosConvertidos = consulta.Dados
            .Select(d => (TDestino)(metodo.Invoke(null, new object[] { d })
                ?? throw new InvalidOperationException("Erro na conversão do operador explicit.")))
            .ToList();

        return new PaginacaoResponse<TDestino>(
            dadosConvertidos,
            consulta.Total,
            consulta.Pagina,
            consulta.TamanhoPagina
        );
    }

}