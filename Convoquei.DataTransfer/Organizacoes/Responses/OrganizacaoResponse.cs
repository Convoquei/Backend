using Convoquei.Core.Organizacoes.Entidades;

namespace Convoquei.DataTransfer.Organizacoes.Responses
{
    public record OrganizacaoResponse(
        Guid Id,
        string Nome,
        MembroOrganizacaoResponse Lider
    )
    {
        public static explicit operator OrganizacaoResponse(Organizacao organizacao)
        {
            OrganizacaoResponse response = new(organizacao.Id, organizacao.Nome, (MembroOrganizacaoResponse)organizacao.Lider);

            return response;
        }
    }
}
