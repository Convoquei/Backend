using Convoquei.Application.Organizacoes.Servicos.Interfaces;
using Convoquei.Core.Genericos.UoW;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Repositorios;
using Convoquei.DataTransfer.Organizacoes.Requests;
using Convoquei.DataTransfer.Organizacoes.Responses;
using Microsoft.Extensions.Logging;

namespace Convoquei.Application.Organizacoes.Servicos
{
    public class OrganizacoesAppServico : IOrganizacoesAppServico
    {
        private readonly ILogger<OrganizacoesAppServico> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganizacoesRepositorio _organizacoesRepositorio;

        public OrganizacoesAppServico(ILogger<OrganizacoesAppServico> logger, IUnitOfWork unitOfWork, IOrganizacoesRepositorio organizacoesRepositorio)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _organizacoesRepositorio = organizacoesRepositorio;
        }

        public async Task<OrganizacaoResponse> CriarAsync(CriarOrganizacaoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                Organizacao organizacao = new(request.Nome);

                await _organizacoesRepositorio.InserirAsync(organizacao, cancellationToken);

                await _unitOfWork.CommitAsync();

                return (OrganizacaoResponse)organizacao;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar {operacao}. Detalhes: {Mensagem}", "CriarOrganizacao", ex.Message);
                throw;
            }
        }
    }
}
