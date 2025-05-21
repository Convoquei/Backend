using Convoquei.DataTransfer.Eventos.Requests;
using Convoquei.DataTransfer.Eventos.Responses;
using Convoquei.DataTransfer.Genericos.Responses;

namespace Convoquei.Application.Eventos.Servicos.Interfaces
{
    public interface IEventosAppServico
    {
        Task<EventoResponse> CriarAsync(Guid idOrganizacao, CriarEventoRequest request, CancellationToken cancellationToken);
        Task<PaginacaoResponse<EventoResponse>> ListarAsync(ListarEventosRequest request, CancellationToken cancellationToken);
        Task<EventoResponse?> RecuperarAsync(Guid id, CancellationToken cancellationToken);
    }
}
