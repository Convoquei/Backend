using Convoquei.DataTransfer.Genericos.Responses;
using Convoquei.DataTransfer.RecorrenciasEvento.Requests;
using Convoquei.DataTransfer.RecorrenciasEvento.Responses;

namespace Convoquei.Application.RecorrenciasEvento.Servicos.Interfaces
{
    public interface IRecorrenciasEventoAppServico
    {
        Task<PaginacaoResponse<RecorrenciaEventoResponse>> ListarAsync(Guid idOrganizacao, int pagina, int tamanhoPagina, CancellationToken cancellationToken);
        Task<RecorrenciaEventoResponse> RecuperarAsync(Guid idOrganizacao, Guid idRecorrencia, CancellationToken cancellationToken);
        Task DeletarAsync(Guid idOrganizacao, Guid idRecorrencia, CancellationToken cancellationToken);
        Task<RecorrenciaEventoResponse> CriarRecorrenciaAsync(Guid idOrganizacao, CriarRecorrenciaRequest request, CancellationToken cancellationToken);
        Task<int> GerarEventosRecorrenciaAsync(Guid idOrganizacao, Guid idRecorrencia, CancellationToken cancellationToken);
    }
}
