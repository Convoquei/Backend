using Convoquei.DataTransfer.RecorrenciasEvento.Requests;
using Convoquei.DataTransfer.RecorrenciasEvento.Responses;

namespace Convoquei.Application.RecorrenciasEvento.Servicos.Interfaces
{
    public interface IRecorrenciasEventoAppServico
    {
        Task<RecorrenciaEventoResponse> CriarRecorrenciaAsync(Guid idOrganizacao, CriarRecorrenciaRequest request, CancellationToken cancellationToken);
    }
}
