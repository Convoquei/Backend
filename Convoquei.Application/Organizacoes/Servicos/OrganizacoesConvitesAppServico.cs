using Convoquei.Application.Autenticacao;
using Convoquei.Application.Genericos;
using Convoquei.Application.Genericos.Extensoes;
using Convoquei.Application.Organizacoes.Servicos.Interfaces;
using Convoquei.Core.Genericos.Repositorios.Consultas;
using Convoquei.Core.Genericos.UoW;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Excecoes;
using Convoquei.Core.Organizacoes.Repositorios;
using Convoquei.Core.Organizacoes.Servicos.Interfaces;
using Convoquei.DataTransfer.Genericos.Responses;
using Convoquei.DataTransfer.Organizacoes.Requests;
using Convoquei.DataTransfer.Organizacoes.Responses;
using Microsoft.Extensions.Logging;

namespace Convoquei.Application.Organizacoes.Servicos
{
    public class OrganizacoesConvitesAppServico : IOrganizacoesConvitesAppServico
    {
        private readonly IOrganizacoesServico _organizacoesServico;
        private readonly IOrganizacoesConvitesServico _organizacoesConvitesServico;
        private readonly IOrganizacoesRepositorio _organizacoesRepositorio;
        private readonly IUsuarioSessao _usuarioSessao;
        private readonly ILogger<OrganizacoesConvitesAppServico> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizacoesConvitesAppServico(IOrganizacoesServico organizacoesServico, IOrganizacoesConvitesServico organizacoesConvitesServico, IOrganizacoesRepositorio organizacoesRepositorio, IUsuarioSessao usuarioSessao, ILogger<OrganizacoesConvitesAppServico> logger, IUnitOfWork unitOfWork)
        {
            _organizacoesServico = organizacoesServico;
            _organizacoesConvitesServico = organizacoesConvitesServico;
            _organizacoesRepositorio = organizacoesRepositorio;
            _usuarioSessao = usuarioSessao;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ConviteOrganizacaoResponse> ConvidarAsync(Guid idOrganizacao, ConvidarMembroRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Organizacao organizacao = await _organizacoesServico.ValidarAsync(idOrganizacao, cancellationToken);

                MembroOrganizacao membro = organizacao.ValidarMembro(_usuarioSessao.Usuario);

                await _unitOfWork.BeginTransactionAsync();

                ConviteOrganizacao convite = organizacao.ConvidarUsuario(membro, request.Email);

                await _unitOfWork.CommitAsync();

                return (ConviteOrganizacaoResponse)convite;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "ConvidarAsync", request);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<PaginacaoResponse<ConviteOrganizacaoResponse>> ListarConvitesAsync(Guid idOrganizacao, ListarConvitesOrganizacaoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _organizacoesServico.ValidarAsync(idOrganizacao, cancellationToken);

                PaginacaoConsulta<ConviteOrganizacao> convites = await _organizacoesRepositorio.ListarConvitesAsync(idOrganizacao, request.Pagina, request.TamanhoPagina, cancellationToken);

                PaginacaoResponse<ConviteOrganizacaoResponse> response = convites.MapearPaginacaoResponse<ConviteOrganizacao, ConviteOrganizacaoResponse>();

                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "ListarConvitesAsync", request);
                throw;
            }
        }

        public async Task<ConviteOrganizacaoResponse> ReenviarConviteAsync(Guid idOrganizacao, Guid idConvite, CancellationToken cancellationToken)
        {
            try
            {
                Organizacao organizacao = await _organizacoesServico.ValidarAsync(idOrganizacao, cancellationToken);
                MembroOrganizacao membro = organizacao.ValidarMembro(_usuarioSessao.Usuario);

                await _unitOfWork.BeginTransactionAsync();

                ConviteOrganizacao convite = organizacao.ValidarConvite(idConvite);
                convite.Reenviar(membro);

                await _unitOfWork.CommitAsync();

                return (ConviteOrganizacaoResponse)convite;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "ReenviarConviteAsync");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task ExcluirConviteAsync(Guid idOrganizacao, Guid idConvite, CancellationToken cancellationToken)
        {
            try
            {
                Organizacao organizacao = await _organizacoesServico.ValidarAsync(idOrganizacao, cancellationToken);
                ConviteOrganizacao convite = organizacao.ValidarConvite(idConvite);
                MembroOrganizacao membro = organizacao.ValidarMembro(_usuarioSessao.Usuario);

                await _unitOfWork.BeginTransactionAsync();

                organizacao.ExcluirConvite(membro, convite);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoverConviteAsync", (idConvite, idOrganizacao));
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<(bool, string)> ResponderConviteAsync(Guid idOrganizacao, Guid idConvite, ResponderConviteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Organizacao organizacao = await _organizacoesServico.ValidarAsync(idOrganizacao, cancellationToken);
                ConviteOrganizacao convite = organizacao.ValidarConvite(idConvite);

                await _unitOfWork.BeginTransactionAsync();

                _organizacoesConvitesServico.ProcessarRespostaConvite(organizacao, convite, _usuarioSessao.Usuario, request.Aceito);

                await _unitOfWork.CommitAsync();

                return (true, $"Convite {(request.Aceito ? "aceito" : "negado")} para a organização {organizacao.Nome}!");
            }
            catch (ConviteExpiradoExcecao ex)
            {
                await _unitOfWork.CommitAsync();
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ReenviarConviteAsync");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
