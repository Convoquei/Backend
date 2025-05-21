using Convoquei.Application.Autenticacao;
using Convoquei.Application.Genericos;
using Convoquei.Application.Organizacoes.Servicos.Interfaces;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Genericos.Repositorios.Consultas;
using Convoquei.Core.Genericos.UoW;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Repositorios;
using Convoquei.Core.Organizacoes.Servicos.Interfaces;
using Convoquei.DataTransfer.Genericos.Responses;
using Convoquei.DataTransfer.Organizacoes.Requests;
using Convoquei.DataTransfer.Organizacoes.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Convoquei.Application.Organizacoes.Servicos
{
    public class OrganizacoesAppServico : IOrganizacoesAppServico
    {
        private readonly ILogger<OrganizacoesAppServico> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganizacoesRepositorio _organizacoesRepositorio;
        private readonly IUsuarioSessao _usuarioSessao;
        private readonly IOrganizacoesServico _organizacoesServico;

        public OrganizacoesAppServico(ILogger<OrganizacoesAppServico> logger, IUnitOfWork unitOfWork, IOrganizacoesRepositorio organizacoesRepositorio, IUsuarioSessao usuarioSessao, IOrganizacoesServico organizacoesServico)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _organizacoesRepositorio = organizacoesRepositorio;
            _usuarioSessao = usuarioSessao;
            _organizacoesServico = organizacoesServico;
        }

        public async Task<OrganizacaoResponse> CriarAsync(CriarOrganizacaoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                Organizacao organizacao = await _organizacoesServico.CriarAsync(request.Nome, _usuarioSessao.Usuario, cancellationToken);

                await _organizacoesRepositorio.InserirAsync(organizacao, cancellationToken);

                await _unitOfWork.CommitAsync();

                return (OrganizacaoResponse)organizacao;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar {operacao}. Detalhes: {Mensagem}", "CriarOrganizacao", ex.Message);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<PaginacaoResponse<OrganizacaoResponse>> ListarAsync(ListarOrganizacoesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Organizacao> query = _organizacoesRepositorio
                    .QueryAsNoTracking()
                    .Include(o => o.Membros).ThenInclude(m => m.Usuario)
                    .Include(o => o.Assinatura).ThenInclude(a => a.Plano);

                PaginacaoConsulta<Organizacao> organizacoes = await _organizacoesRepositorio.ListarAsync(query, request.Pagina, request.TamanhoPagina, cancellationToken);
                
                return organizacoes.MapearPaginacaoResponse<Organizacao, OrganizacaoResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar {operacao}. Detalhes: {Mensagem}", "ListarOrganização", ex.Message);
                throw;
            }
        }

        public async Task<OrganizacaoResponse?> RecuperarAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                Organizacao organizacao = await _organizacoesServico.ValidarAsync(id, cancellationToken);

                return (OrganizacaoResponse)organizacao;
            }
            catch(EntidadeNaoEncontradaExcecao)
            {
                return null;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar {operacao}. Detalhes: {Mensagem}", "RecuperarOrganização", ex.Message);
                throw;
            }
        }
    }
}
