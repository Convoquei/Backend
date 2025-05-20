using Convoquei.Application.Usuarios.Excecoes;
using Convoquei.Application.Usuarios.Servicos.Interfaces;
using Convoquei.Core.Genericos.UoW;
using Convoquei.Core.Seguranca.Servicos;
using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.Repositorios;
using Convoquei.Core.Usuarios.Servicos.Interfaces;
using Convoquei.Core.Usuarios.ValueObjects;
using Convoquei.DataTransfer.Usuarios.Request;
using Convoquei.DataTransfer.Usuarios.Response;
using Microsoft.Extensions.Logging;

namespace Convoquei.Application.Usuarios.Servicos
{
    public class AutenticacaoAppServico : IAutenticacaoAppServico
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly ILogger<AutenticacaoAppServico> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICriarUsuarioServico _criarUsuarioServico;
        private readonly ICriptografiaServico _criptografiaServico;
        private readonly ITokenServico _tokenServico;

        public AutenticacaoAppServico(IUsuariosRepositorio usuariosRepositorio, ILogger<AutenticacaoAppServico> logger, IUnitOfWork unitOfWork, ICriarUsuarioServico criarUsuarioServico, ICriptografiaServico criptografiaServico, ITokenServico tokenServico)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _criarUsuarioServico = criarUsuarioServico;
            _criptografiaServico = criptografiaServico;
            _tokenServico = tokenServico;
        }

        public async Task<AutenticadoResponse> AutenticarAsync(LogarRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Usuario? usuario = await _usuariosRepositorio.RecuperarAsync(usuario => usuario.Email.Endereco == request.Email, cancellationToken);
                if(usuario is null || !_criptografiaServico.Validar(usuario.Senha, request.Senha))
                {
                    throw new CredenciaisInvalidasExcecao("As credenciais informadas são inválidas.");
                }

                Token token = _tokenServico.GerarToken(usuario);

                await _unitOfWork.BeginTransactionAsync();

                usuario.SetToken(token);

                await _unitOfWork.CommitAsync();

                return (AutenticadoResponse)usuario;
            }
            catch(CredenciaisInvalidasExcecao)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<UsuarioResponse> CadastrarAsync(CadastrarRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                Usuario usuario = await _criarUsuarioServico.CriarAsync(request.Nome, request.Email, _criptografiaServico.Criptografar(request.Senha), cancellationToken);

                await _usuariosRepositorio.InserirAsync(usuario, cancellationToken);

                await _unitOfWork.CommitAsync();

                return (UsuarioResponse)usuario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao executar operação de {operacao}", "CadastrarAsync");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
