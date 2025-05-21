using Convoquei.Core.Assinaturas.Entidades;
using Convoquei.DataTransfer.Comuns.Responses;

namespace Convoquei.DataTransfer.Assinaturas.Responses
{
    public record PlanoResponse(
        string Nome,
        int LimiteMembros,
        int LimiteEventosMensais,
        decimal Valor,
        GenericoEnumResponse<int> Tipo
    )
    {
        public static explicit operator PlanoResponse(Plano plano)
        {
            PlanoResponse response = new(
                plano.Nome,
                plano.LimiteMembros,
                plano.LimiteEventosMensais,
                plano.Valor,
                (GenericoEnumResponse<int>)plano.Tipo
            );
            return response;
        }
    }
}