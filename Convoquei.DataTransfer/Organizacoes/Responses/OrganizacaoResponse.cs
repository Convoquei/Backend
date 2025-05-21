using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.DataTransfer.Assinaturas.Responses;

namespace Convoquei.DataTransfer.Organizacoes.Responses
{
    public record OrganizacaoResponse(
        Guid Id,
        string Nome,
        AssinaturaResponse Assinatura,
        MembroOrganizacaoResponse Lider,
        IEnumerable<MembroOrganizacaoResponse> Membros
    )
    {
        public static explicit operator OrganizacaoResponse(Organizacao organizacao)
        {
            OrganizacaoResponse response = new(
                organizacao.Id,
                organizacao.Nome,
                (AssinaturaResponse)organizacao.Assinatura,
                (MembroOrganizacaoResponse)organizacao.Lider,
                organizacao.Membros.Select(m => (MembroOrganizacaoResponse)m)
            );

            return response;
        }
    }
}
