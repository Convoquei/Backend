using Convoquei.Core.Assinaturas.Entidades;

namespace Convoquei.DataTransfer.Assinaturas.Responses
{
    public record AssinaturaResponse(
        PlanoResponse Plano,
        DateTime DataInicio,
        DateTime? DataFim,
        bool Ativa
    )
    {
        public static explicit operator AssinaturaResponse(Assinatura assinatura)
        {
            AssinaturaResponse response = new((PlanoResponse)assinatura.Plano, assinatura.DataInicio, assinatura.DataFim, assinatura.Ativa);

            return response;
        }
    }
}
