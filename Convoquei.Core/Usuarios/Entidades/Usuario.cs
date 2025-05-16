using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.ValueObjects;
using Convoquei.Core.Organizacoes.Entidades;

namespace Convoquei.Core.Usuarios.Entidades
{
    public sealed class Usuario : EntidadeBase
    {
        public string Nome { get; private set; }
        public Email Email { get; private set; }

        private readonly List<Organizacao> _organizacoes = new();
        public IReadOnlyCollection<Organizacao> Organizacoes => _organizacoes.AsReadOnly();

        public Usuario(string nome, Email email) : base(Guid.NewGuid())
        {
            Nome = nome;
            Email = email;
        }

        private Usuario() { }

        public void AdicionarOrganizacao(Organizacao organizacao)
        {
            _organizacoes.Add(organizacao);
        }

        public void RemoverOrganizacao(Organizacao organizacao)
        {
            _organizacoes.Remove(organizacao);
        }
    }
}
