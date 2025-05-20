using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.Repositorios;
using Convoquei.Core.Usuarios.Servicos.Interfaces;

namespace Convoquei.Core.Usuarios.Servicos
{
    public class CriarUsuarioServico : ICriarUsuarioServico
    {
        public readonly IUsuariosRepositorio _usuariosRepositorio;

        public CriarUsuarioServico(IUsuariosRepositorio usuariosRepositorio)
        {
            _usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<Usuario> CriarAsync(string nome, string email, string senhaHash, CancellationToken cancellationToken)
        {
            await ValidarEmailDuplicado(email, cancellationToken);

            Usuario usuario = new Usuario(nome, senhaHash, email);

            return usuario;
        }

        private async Task ValidarEmailDuplicado(string email, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _usuariosRepositorio.RecuperarAsync(usuario => usuario.Email.Endereco == email.ToLowerInvariant(), cancellationToken);
            if (usuario is not null)
                throw new RegraDeNegocioExcecao($"O e-mail {usuario.Email.Endereco} já está cadastrado no sistema.");
        }
    }
}
