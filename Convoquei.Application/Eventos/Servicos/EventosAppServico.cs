using Convoquei.Application.Autenticacao;
using Convoquei.Application.Eventos.Servicos.Interfaces;
using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Eventos.Repositorios;
using Convoquei.Core.Eventos.Servicos.Comandos;
using Convoquei.Core.Eventos.Servicos.Interfaces;
using Convoquei.Core.Genericos.UoW;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Servicos.Interfaces;
using Convoquei.Core.Usuarios.Entidades;
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
        private readonly IOrganizacoesServico _organizacoesServico;

        public EventosAppServico(ILogger<EventosAppServico> logger, IUnitOfWork unitOfWork, IEventosRepositorio eventosRepositorio, IUsuarioSessao usuarioSessao, IEventosServico eventosServico, IOrganizacoesServico organizacoesServico)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _eventosRepositorio = eventosRepositorio;
            _usuarioSessao = usuarioSessao;
            _eventosServico = eventosServico;
            _organizacoesServico = organizacoesServico;
        }

        public async Task<EventoResponse> CriarAsync(Guid idOrganizacao, CriarEventoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                Usuario usuario = _usuarioSessao.Usuario;

                Organizacao organizacao = await _organizacoesServico.ValidarAsync(idOrganizacao, cancellationToken);

                MembroOrganizacao membro = organizacao.ValidarMembro(usuario);

                CriarEventoComando comando = new(
                    request.Nome,
                    request.Local,
                    request.Descricao,
                    (TipoEventoEnum)request.Tipo,
                    (StatusEventoEnum)request.Status,
                    request.DataHoraInicio,
                    request.FechamentoEscalaAntecedencia,
                    membro,
                    organizacao
                );

                Evento evento = _eventosServico.CriarEvento(comando, cancellationToken);

                await _unitOfWork.CommitAsync();

                return (EventoResponse)evento;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriarEventoAsync", request);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public Task<PaginacaoResponse<EventoResponse>> ListarAsync(ListarEventosRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<EventoResponse?> RecuperarAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
