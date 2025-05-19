using Convoquei.Core.Assinaturas.Entidades;
using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Enumeradores;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Entidades
{
    public sealed class Organizacao : EntidadeBase
    {
        public string Nome { get; private set; }
        public Assinatura Assinatura { get; private set; }
        public bool ExigirAprovacaoDisponibilidade { get; private set; }

        private readonly HashSet<ConviteOrganizacao> _convites = new();
        public IReadOnlyCollection<ConviteOrganizacao> Convites => _convites;

        private readonly HashSet<MembroOrganizacao> _membros = new();
        public IReadOnlyCollection<MembroOrganizacao> Membros => _membros;

        private IList<Evento> _eventos = new List<Evento>();
        public IReadOnlyCollection<Evento> Eventos => _eventos.AsReadOnly();

        public Organizacao(string nome)
        {
            Nome = nome;
            Assinatura = Assinatura.CriarGratuita(this);
            ExigirAprovacaoDisponibilidade = false;
        }

        public void AdicionarEvento(Evento evento)
        {
            if (_eventos.Any(e => e.Id == evento.Id))
                throw new RegraDeNegocioExcecao("Evento já cadastrado na organização.");

            _eventos.Add(evento);
        }

        public void ConvidarUsuario(MembroOrganizacao membro, string email)
        {
            if (!membro.PossuiPermissoesAdministrativas(this))
                throw new RegraDeNegocioExcecao("É necessário possuir permissões administrativas na organização para convidar usuários.");

            ConviteOrganizacao convite = new(email, this, DateTime.UtcNow.AddDays(7), membro.Usuario);
            _convites.Add(convite);
        }

        public void AceitarConvite(Usuario usuario, ConviteOrganizacao convite)
        {
            if(!usuario.Email.Equals(convite.Email))
                throw new RegraDeNegocioExcecao("Convite não pertence ao usuário.");
            if (convite.DataExpiracao < DateTime.UtcNow)
                throw new RegraDeNegocioExcecao("Convite expirado.");

            MembroOrganizacao membro = new(usuario, this, CargoOrganizacaoEnum.Membro);
            _membros.Add(membro);
            _convites.Remove(convite);
        }
    }
}
