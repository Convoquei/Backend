using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.DataTransfer.Organizacoes.Responses
{
    public record ConviteOrganizacaoResponse(
        Guid Id,
        string Email,
        DateTime DataCriacao,
        DateTime DataExpiracao,
        bool PermiteReenviar,
        UsuarioResponse Convidador
    )
    {
        public static explicit operator ConviteOrganizacaoResponse(ConviteOrganizacao convite)
        {
            ConviteOrganizacaoResponse response = new(convite.Id, convite.Email.Endereco, convite.DataCriacao, convite.DataExpiracao, convite.PermiteReenvio, (UsuarioResponse)convite.Convidador);

            return response;
        }
    }
}
