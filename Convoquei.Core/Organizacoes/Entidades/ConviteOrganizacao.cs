using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.ValueObjects;

namespace Convoquei.Core.Organizacoes.Entidades
{
    public class ConviteOrganizacao : EntidadeBase
    {
        public Email Email { get; private set; }
        public virtual Organizacao Organizacao { get; private set; }
        public DateTime DataExpiracao { get; private set; }
        public virtual Usuario Convidador { get; private set; }

        public ConviteOrganizacao(Email email, Organizacao organizacao, DateTime dataExpiracao, Usuario convidador)
        {
            Email = email;
            Organizacao = organizacao;
            DataExpiracao = dataExpiracao;
            Convidador = convidador;
        }

        protected ConviteOrganizacao()
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
