using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Eventos.ValueObjects;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Genericos.Extensoes;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.Entidades
{
    public class Evento : EntidadeBase
    {
        public string Nome { get; private set; }
        public string Local { get; private set; }
        public string Descricao { get; private set; }
        public TipoEventoEnum Tipo { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public TimeSpan FechamentoEscalaAntecedencia { get; private set; }
        public virtual Usuario Criador { get; private set; }
        public virtual Organizacao Organizacao { get; private set; }
        public StatusEventoEnum Status { get; private set; }
        public virtual DadosCancelamentoEvento? Cancelamento { get; private set; }
        public virtual HashSet<ArquivoEvento> Arquivos { get; private set; }
        public virtual HashSet<ParticipanteEvento> Participantes { get; private set; }

        public bool Cancelado => Status == StatusEventoEnum.Cancelado;

        public Evento(string nome, string local, string descricao, TipoEventoEnum tipo, DateTime dataHoraInicio, TimeSpan fechamentoEscalaAntecedencia, MembroOrganizacao membroCriador, Organizacao organizacao)
        {
            if(!membroCriador.PossuiPermissoesAdministrativas(organizacao))
                throw new RegraDeNegocioExcecao("É necessário possuir permissões administrativas na organização para criar eventos.");

            Nome = nome;
            Local = local;
            Descricao = descricao;
            Tipo = tipo;
            DataHoraInicio = dataHoraInicio;
            FechamentoEscalaAntecedencia = fechamentoEscalaAntecedencia;
            Criador = membroCriador.Usuario;
            Organizacao = organizacao;
            Status = StatusEventoEnum.Ativo;
            Cancelamento = null;

            PopularParticipantesIniciais();
        }

        protected Evento()
        {
        }

        private void PopularParticipantesIniciais()
        {
            IEnumerable<ParticipanteEvento> participantes = Organizacao
                .Membros
                .Select(membro => new ParticipanteEvento(membro.Usuario, this, StatusParticipacaoEventoEnum.NaoInformado));

            Participantes.AddRange(participantes);
        }

        public void Cancelar(MembroOrganizacao membroCancelando, string motivo)
        {
            if (!membroCancelando.PossuiPermissoesAdministrativas(Organizacao))
                throw new RegraDeNegocioExcecao("Somente administradores da organização podem cancelar eventos.");
            if (Status == StatusEventoEnum.Cancelado)
                throw new RegraDeNegocioExcecao($"Esse evento já está cancelado! ({Cancelamento})");
            if (DataHoraInicio < DateTime.Now)
                throw new RegraDeNegocioExcecao("Eventos passados não podem ser cancelados.");

            Status = StatusEventoEnum.Cancelado;
            Cancelamento = new DadosCancelamentoEvento(DateTime.UtcNow, motivo, membroCancelando.Usuario);
        }
    }
}
