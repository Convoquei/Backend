using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Enumeradores.CargoMembroEnum;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Entidades
{
    public sealed class Organizacao : EntidadeBase
    {
        public string Nome { get; private set; }
        public Usuario Criador => Membros.FirstOrDefault(m => m.Cargo == CargoMembroEnum.Criador)!.Usuario;

        private readonly HashSet<MembroOrganizacao> _membros = new();
        public IReadOnlyCollection<MembroOrganizacao> Membros => _membros;

        private readonly HashSet<EventoBase> _eventos = new();
        public IReadOnlyCollection<EventoBase> Eventos => _eventos;

        public Organizacao(string nome, Usuario usuario) : base(Guid.NewGuid())
        {
            Nome = nome;
            
            MembroOrganizacao membroCriador = new(usuario, this, CargoMembroEnum.Criador);
            _membros.Add(membroCriador);
        }

        private Organizacao() { }

        public void AdicionarMembro(Usuario usuario)
        {
            MembroOrganizacao membro = new(usuario, this, CargoMembroEnum.Membro);

            if (!_membros.Add(membro))
                throw new RegraDeNegocioExcecao($"O membro {membro} já pertence à organização.");

            usuario.AdicionarOrganizacao(this);
            PopularPossivelParticipacaoEventosAtivos(membro);
        }

        public void RemoverMembro(Usuario usuario)
        {
            MembroOrganizacao? membro = _membros.FirstOrDefault(m => m.Usuario.Equals(usuario));
            if (membro is null)
                throw new RegraDeNegocioExcecao($"O membro {membro} não pertence à organização.");

            _membros.Remove(membro);
            RemoverParticipacoesEventoAtivos(membro);
            usuario.RemoverOrganizacao(this);
        }

        public void Deletar(Usuario usuario)
        {
            if(!usuario.Equals(Criador))
                throw new RegraDeNegocioExcecao("Apenas o criador da organização pode deletá-la.");

            bool possuiEventosAtivos = Eventos.Any(e => e.Status == StatusEventoEnum.Ativo);

            if (possuiEventosAtivos)
                throw new RegraDeNegocioExcecao("A organização não pode ser deletada enquanto houver eventos ativos.");
        }

        private void PopularPossivelParticipacaoEventosAtivos(MembroOrganizacao membro)
        {
            foreach (EventoBase evento in _eventos)
            {
                if (evento.Status != StatusEventoEnum.Ativo)
                    continue;

                evento.AdicionarParticipante(membro);
            }
        }

        private void RemoverParticipacoesEventoAtivos(MembroOrganizacao membro)
        {
            foreach (EventoBase evento in _eventos)
            {
                if(evento.Status != StatusEventoEnum.Ativo)
                    continue;

                evento.RemoverParticipante(membro);
            }
        }

        public static implicit operator string(Organizacao organizacao)
            => organizacao.Nome;
    }
}
