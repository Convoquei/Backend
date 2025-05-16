using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Eventos.ValueObjects;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.Entidades
{
    public abstract class EventoBase : EntidadeBase
    {
        private const int VALOR_MAXIMO_MINUTOS_FECHAMENTO_AUTOMATICO = 31 * 24 * 60;

        public string Nome { get; private set; }
        public CategoriaEventoEnum Categoria { get; private set; }
        public string Local { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataHora { get; private set; }
        public Organizacao Organizacao { get; private set; }
        public Usuario Criador { get; private set; }
        public StatusEventoEnum Status { get; private set; }
        public StatusEscalaEventoEnum StatusEscala { get; private set; }
        public DelecaoEvento? DadosDelecao { get; private set; }
        public abstract TipoEventoEnum Tipo { get; }
        public int MinutosAntesParaFechamentoAutomatico { get; private set; }
        private readonly HashSet<ParticipanteEvento> _participacoes = new();
        public IReadOnlyCollection<ParticipanteEvento> Participacoes => _participacoes;
        public IEnumerable<Usuario> MembrosEscalados => Participacoes.Where(p => p.Status == StatusParticipacaoEventoEnum.Escalado).Select(p => p.Usuario);

        protected EventoBase(
            string nome,
            CategoriaEventoEnum categoria,
            string local,
            string descricao,
            DateTime dataHora,
            Organizacao organizacao,
            MembroOrganizacao membroCriador,
            int minutosAntesParaFechamentoAutomatico) : base(Guid.NewGuid())
        {
            membroCriador.ValidarPermissaoAdministrativa(organizacao);
            if (minutosAntesParaFechamentoAutomatico < 0 || minutosAntesParaFechamentoAutomatico > VALOR_MAXIMO_MINUTOS_FECHAMENTO_AUTOMATICO)
            {
                TimeSpan limite = TimeSpan.FromMinutes(VALOR_MAXIMO_MINUTOS_FECHAMENTO_AUTOMATICO);
                throw new RegraDeNegocioExcecao($"O valor de minutos para fechamento automático deve ser no máximo {limite.Days} dias, {limite.Hours} horas e {limite.Minutes} minutos.");
            }

            Nome = nome;
            Categoria = categoria;
            Local = local;
            Descricao = descricao;
            DataHora = dataHora;
            Organizacao = organizacao;
            Criador = membroCriador.Usuario;
            Status = StatusEventoEnum.Ativo;
            StatusEscala = StatusEscalaEventoEnum.Aberta;
            MinutosAntesParaFechamentoAutomatico = minutosAntesParaFechamentoAutomatico;

            PopularParticipantesInicial();
        }

        private EventoBase() { }

        private void PopularParticipantesInicial()
        {
            foreach (MembroOrganizacao membro in Organizacao.Membros)
            {
                ParticipanteEvento participacao = new(membro.Usuario, this, membro.Cargo);
                _participacoes.Add(participacao);
            }
        }

        public void DeletarEvento(MembroOrganizacao membro, DateTime data, string motivo)
        {
            membro.ValidarPermissaoAdministrativa(Organizacao);

            DadosDelecao = new DelecaoEvento(membro.Usuario, data, motivo);
            Status = StatusEventoEnum.Deletado;
        }

        public void AdicionarParticipante(MembroOrganizacao membro)
        {
            if (Status != StatusEventoEnum.Ativo)
                throw new RegraDeNegocioExcecao("Não é permitido adicionar participante a um evento que não esteja ativo.");

            if(membro.Organizacao.Id != Organizacao.Id)
                throw new RegraDeNegocioExcecao($"O membro {membro} não pertence à organização do evento.");
            if (Participacoes.Any(p => p.Usuario.Id == membro.Usuario.Id))
                throw new RegraDeNegocioExcecao($"O membro {membro} já está participando do evento.");

            ParticipanteEvento participacao = new(membro.Usuario, this, membro.Cargo);

            _participacoes.Add(participacao);
        }

        public void RemoverParticipante(MembroOrganizacao membro)
        {
            if (Status != StatusEventoEnum.Ativo)
                throw new RegraDeNegocioExcecao("Não é permitido remover participante de um evento que não esteja ativo.");

            ParticipanteEvento? participacao = Participacoes.FirstOrDefault(p => p.Usuario.Id == membro.Usuario.Id);
            if (participacao is not null)
                _participacoes.Remove(participacao);
        }
    }
}
