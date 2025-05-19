using Convoquei.DataTransfer.Organizacoes.Requests;
using Convoquei.DataTransfer.Organizacoes.Responses;

namespace Convoquei.Application.Organizacoes.Servicos.Interfaces
{
    public interface IOrganizacoesAppServico
    {
        Task<OrganizacaoResponse> CriarAsync(CriarOrganizacaoRequest request, CancellationToken cancellationToken);
    }
}
