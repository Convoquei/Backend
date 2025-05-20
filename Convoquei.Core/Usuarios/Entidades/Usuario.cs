using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Usuarios.ValueObjects;

namespace Convoquei.Core.Usuarios.Entidades
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public Email Email { get; private set; }
        public Token? Token { get; private set; }

        public Usuario(string nome, string senha, Email email)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Token = null;
        }

        private Usuario() { }

        public void SetToken(Token token)
        {
            Token = token;
        }

        public static implicit operator string(Usuario usuario)
        {
            return usuario.Nome;
        }
    }
}
