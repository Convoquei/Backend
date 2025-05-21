using Convoquei.DataTransfer.Genericos.Responses;
using Convoquei.DataTransfer.Organizacoes.Requests;
using Convoquei.DataTransfer.Organizacoes.Responses;

namespace Convoquei.Application.Organizacoes.Servicos.Interfaces
{
    public interface IOrganizacoesAppServico
    {
        Task<OrganizacaoResponse> CriarAsync(CriarOrganizacaoRequest request, CancellationToken cancellationToken);
        Task<PaginacaoResponse<OrganizacaoResponse>> ListarAsync(ListarOrganizacoesRequest request, CancellationToken cancellationToken);
        Task<OrganizacaoResponse?> RecuperarAsync(Guid id, CancellationToken cancellationToken);
    }
}
