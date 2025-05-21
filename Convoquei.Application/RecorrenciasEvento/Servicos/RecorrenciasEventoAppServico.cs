using Convoquei.Application.Autenticacao;
using Convoquei.Application.Genericos.Extensoes;
using Convoquei.Application.RecorrenciasEvento.Servicos.Interfaces;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.UoW;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Servicos.Interfaces;
using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.RecorrenciasEvento.Enumeradores;
using Convoquei.Core.RecorrenciasEvento.Servicos.Comandos;
using Convoquei.Core.RecorrenciasEvento.Servicos.Interfaces;
using Convoquei.Core.Usuarios.Entidades;
using Convoquei.DataTransfer.RecorrenciasEvento.Requests;
using Convoquei.DataTransfer.RecorrenciasEvento.Responses;
using Microsoft.Extensions.Logging;

namespace Convoquei.Application.RecorrenciasEvento.Servicos
{
    public class RecorrenciasEventoAppServico : IRecorrenciasEventoAppServico
    {
        private readonly IOrganizacoesServico _organizacoesServico;
        private readonly IRecorrenciasEventoServico _recorrenciasEventoServico;
        private readonly ILogger<RecorrenciasEventoAppServico> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioSessao _usuarioSessao;

        public RecorrenciasEventoAppServico(IOrganizacoesServico organizacoesServico, IRecorrenciasEventoServico recorrenciasEventoServico, ILogger<RecorrenciasEventoAppServico> logger, IUnitOfWork unitOfWork, IUsuarioSessao usuarioSessao)
        {
            _organizacoesServico = organizacoesServico;
            _recorrenciasEventoServico = recorrenciasEventoServico;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _usuarioSessao = usuarioSessao;
        }

        public async Task<RecorrenciaEventoResponse> CriarRecorrenciaAsync(Guid idOrganizacao, CriarRecorrenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Organizacao organizacao = await _organizacoesServico.ValidarAsync(idOrganizacao, cancellationToken);

                CriarRecorrenciaEventoComando comando = GerarComandoCriarRecorrencia(organizacao, _usuarioSessao.Usuario, request);

                await _unitOfWork.BeginTransactionAsync();

                RecorrenciaEventoBase recorrenciaCriada = _recorrenciasEventoServico.CriarRecorrencia(comando);

                await _unitOfWork.CommitAsync();

                return (RecorrenciaEventoResponse)recorrenciaCriada;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriarRecorrenciaAsync", request);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private static CriarRecorrenciaEventoComando GerarComandoCriarRecorrencia(Organizacao organizacao, Usuario usuario, CriarRecorrenciaRequest request)
            => new(request.Nome, request.Local, request.Descricao, request.DataHoraInicio, TimeSpan.FromHours(request.HorasFechamentoEscalaAntecedencia), usuario, organizacao, (TipoEventoEnum)request.TipoEvento, request.IntervaloDias, (DiasEventoEnumFlag)(request.DiasSemanaBitmap ?? 0));
    }
}
