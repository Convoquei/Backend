using Convoquei.Core.Organizacoes.Entidades;

namespace Convoquei.DataTransfer.Organizacoes.Responses
{
    public record OrganizacaoResponse(
        Guid Id,
        string Nome
    )
    {
        public static explicit operator OrganizacaoResponse(Organizacao organizacao)
        {
            OrganizacaoResponse response = new(organizacao.Id, organizacao.Nome);

            return response;
        }
    }
}
