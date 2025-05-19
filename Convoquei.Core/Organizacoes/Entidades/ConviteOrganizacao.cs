using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.ValueObjects;

namespace Convoquei.Core.Organizacoes.Entidades
{
    public sealed class ConviteOrganizacao : EntidadeBase
    {
        public Email Email { get; private set; }
        public Organizacao Organizacao { get; private set; }
        public DateTime DataExpiracao { get; private set; }
        public Usuario Convidador { get; private set; }

        public ConviteOrganizacao(Email email, Organizacao organizacao, DateTime dataExpiracao, Usuario convidador)
        {
            Email = email;
            Organizacao = organizacao;
            DataExpiracao = dataExpiracao;
            Convidador = convidador;
        }

        private ConviteOrganizacao()
        {
            
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ConviteOrganizacao outro) return false;

            return Email.Equals(outro.Email) && Organizacao.Id == outro.Organizacao.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email, Organizacao.Id);
        }
    }
}
