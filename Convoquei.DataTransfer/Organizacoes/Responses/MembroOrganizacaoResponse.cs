using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.DataTransfer.Comuns.Responses;

namespace Convoquei.DataTransfer.Organizacoes.Responses
{
    public record MembroOrganizacaoResponse(
        Guid Id,
        string Nome,
        GenericoEnumResponse<int> Cargo
    )
    {
        public static explicit operator MembroOrganizacaoResponse(MembroOrganizacao membro)
        {
            MembroOrganizacaoResponse response = new(membro.Id, membro.Usuario.Nome, membro.Cargo);

            return response;
        }
    }
}