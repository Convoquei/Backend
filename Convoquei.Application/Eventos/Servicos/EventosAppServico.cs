using Convoquei.Application.Autenticacao;
using Convoquei.Application.Eventos.Servicos.Interfaces;
using Convoquei.Core.Eventos.Repositorios;
using Convoquei.Core.Eventos.Servicos.Interfaces;
using Convoquei.Core.Genericos.UoW;
using Convoquei.DataTransfer.Eventos.Requests;
using Convoquei.DataTransfer.Eventos.Responses;
using Convoquei.DataTransfer.Genericos.Responses;
using Microsoft.Extensions.Logging;

namespace Convoquei.Application.Eventos.Servicos
{
    public class EventosAppServico : IEventosAppServico
    {
        private readonly ILogger<EventosAppServico> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventosRepositorio _eventosRepositorio;
        private readonly IUsuarioSessao _usuarioSessao;
        private readonly IEventosServico _eventosServico;

        public EventosAppServico(
            ILogger<EventosAppServico> logger,
            IUnitOfWork unitOfWork,
            IEventosRepositorio eventosRepositorio,
            IUsuarioSessao usuarioSessao,
            IEventosServico eventosServico)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _eventosRepositorio = eventosRepositorio;
            _usuarioSessao = usuarioSessao;
            _eventosServico = eventosServico;
        }

        public async Task<EventoResponse> CriarAsync(CriarEventoRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginacaoResponse<EventoResponse>> ListarAsync(ListarEventosRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<EventoResponse?> RecuperarAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
