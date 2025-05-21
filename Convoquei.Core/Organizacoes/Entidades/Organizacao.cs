using Convoquei.Core.Assinaturas.Entidades;
using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Enumeradores;
using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.Usuarios.Entidades;
using System.Runtime.InteropServices.Marshalling;

namespace Convoquei.Core.Organizacoes.Entidades
{
    public  class Organizacao : EntidadeBase
    {
        public string Nome { get; private set; }
        public bool ExigirAprovacaoDisponibilidade { get; private set; }
        public virtual Assinatura Assinatura { get; private set; }
        public virtual IList<ConviteOrganizacao> Convites { get; private set; } = new List<ConviteOrganizacao>();
        public virtual HashSet<MembroOrganizacao> Membros { get; private set; } = new();
        public virtual IList<Evento> Eventos { get; private set; } = new List<Evento>();
        public virtual IList<RecorrenciaEventoBase> Recorrencias { get; private set; } = new List<RecorrenciaEventoBase>();

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

        public ConviteOrganizacao ConvidarUsuario(MembroOrganizacao membro, string email)
        {
            if (!membro.PossuiPermissoesAdministrativas(this))
                throw new RegraDeNegocioExcecao("É necessário possuir permissões administrativas na organização para convidar usuários.");
            if(Convites.Any(c => c.Email.Endereco.Equals(email, StringComparison.OrdinalIgnoreCase)))
                throw new RegraDeNegocioExcecao("Convite já enviado para o usuário, aguarde a confirmação.");
            if (Membros.Any(m => m.Usuario.Email.Endereco.Equals(email, StringComparison.OrdinalIgnoreCase)))
                throw new RegraDeNegocioExcecao("Já existe um membro na organizacao com esse e-mail.");

            ConviteOrganizacao convite = new(email, this, DateTime.UtcNow.AddDays(7), membro.Usuario);
            Convites.Add(convite);

            return convite;
        }

        public void AdicionarMembro(MembroOrganizacao membro)
        {
            if (Membros.Any(m => m.Usuario.Equals(membro.Usuario)))
                throw new RegraDeNegocioExcecao("Usuário já é membro da organização.");

            Membros.Add(membro);
        }

        public void ExcluirConvite(MembroOrganizacao membroRemovendo, ConviteOrganizacao convite)
        {
            membroRemovendo.ValidarPermissoesAdministrativas();

            Convites.Remove(convite);
        }

        public MembroOrganizacao ValidarMembro(Usuario usuario)
        {
            MembroOrganizacao? membro = Membros.FirstOrDefault(m => m.Usuario.Equals(usuario));

            if (membro == null)
                throw new RegraDeNegocioExcecao("Usuário não é membro da organização.");

            return membro;
        }

        public ConviteOrganizacao ValidarConvite(Guid id)
        {
            ConviteOrganizacao? convite = Convites.FirstOrDefault(c => c.Id == id);
            if (convite == null)
                throw new RegraDeNegocioExcecao("Convite não encontrado.");

            return convite;
        }
    }
}
