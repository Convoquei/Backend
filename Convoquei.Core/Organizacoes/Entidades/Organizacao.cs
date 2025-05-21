using Convoquei.Core.Assinaturas.Entidades;
using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Enumeradores;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Entidades
{
    public  class Organizacao : EntidadeBase
    {
        public string Nome { get; private set; }
        public bool ExigirAprovacaoDisponibilidade { get; private set; }
        public virtual Assinatura Assinatura { get; private set; }
        public virtual HashSet<ConviteOrganizacao> Convites { get; private set; } = new();
        public virtual HashSet<MembroOrganizacao> Membros { get; private set; } = new();
        public virtual IList<Evento> Eventos { get; private set; } = new List<Evento>();

        public MembroOrganizacao Lider => Membros.First(m => m.Cargo == CargoOrganizacaoEnum.Criador);

        public Organizacao(string nome, Plano planoInicial, Usuario usuarioCriador)
        {
            Nome = nome;
            Assinatura = new(planoInicial, this, DateTime.UtcNow, null);
            ExigirAprovacaoDisponibilidade = false;

            MembroOrganizacao membroCriador = new(usuarioCriador, this, CargoOrganizacaoEnum.Criador);
            Membros.Add(membroCriador);
        }

        protected Organizacao()
        {
            
        }

        public void AdicionarEvento(Evento evento)
        {
            if (Eventos.Any(e => e.Id == evento.Id))
                throw new RegraDeNegocioExcecao("Evento já cadastrado na organização.");

            Eventos.Add(evento);
        }

        public void ConvidarUsuario(MembroOrganizacao membro, string email)
        {
            if (!membro.PossuiPermissoesAdministrativas(this))
                throw new RegraDeNegocioExcecao("É necessário possuir permissões administrativas na organização para convidar usuários.");

            ConviteOrganizacao convite = new(email, this, DateTime.UtcNow.AddDays(7), membro.Usuario);
            Convites.Add(convite);
        }

        public void AceitarConvite(Usuario usuario, ConviteOrganizacao convite)
        {
            if(!usuario.Email.Equals(convite.Email))
                throw new RegraDeNegocioExcecao("Convite não pertence ao usuário.");
            if (convite.DataExpiracao < DateTime.UtcNow)
                throw new RegraDeNegocioExcecao("Convite expirado.");

            MembroOrganizacao membro = new(usuario, this, CargoOrganizacaoEnum.Membro);
            Membros.Add(membro);
            Convites.Remove(convite);
        }
    }
}
