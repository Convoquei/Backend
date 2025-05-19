using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Usuarios.ValueObjects;

namespace Convoquei.Core.Usuarios.Entidades
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; private set; }
        public Email Email { get; private set; }

        public HashSet<Organizacao> _organizacoes = new();
        public IReadOnlyCollection<Organizacao> Organizacoes => _organizacoes;

        public Usuario(string nome, Email email)
        {
            Nome = nome;
            Email = email;
        }

        public static implicit operator string(Usuario usuario)
        {
            return usuario.Nome;
        }
    }
}
