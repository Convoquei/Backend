using Convoquei.DataTransfer.Genericos.Responses;
using Convoquei.DataTransfer.Organizacoes.Requests;
using Convoquei.DataTransfer.Organizacoes.Responses;

namespace Convoquei.Application.Organizacoes.Servicos.Interfaces
{
    public interface IOrganizacoesConvitesAppServico
    {
        Task<ConviteOrganizacaoResponse> ConvidarAsync(Guid idOrganizacao, ConvidarMembroRequest request, CancellationToken cancellationToken);
        Task<PaginacaoResponse<ConviteOrganizacaoResponse>> ListarConvitesAsync(Guid idOrganizacao, ListarConvitesOrganizacaoRequest request, CancellationToken cancellationToken);
        Task<ConviteOrganizacaoResponse> ReenviarConviteAsync(Guid idOrganizacao, Guid idConvite, CancellationToken cancellationToken);
        Task<(bool, string)> ResponderConviteAsync(Guid idOrganizacao, Guid idConvite, ResponderConviteRequest request, CancellationToken cancellationToken);
        Task ExcluirConviteAsync(Guid idOrganizacao, Guid idConvite, CancellationToken cancellationToken);
    }
}
