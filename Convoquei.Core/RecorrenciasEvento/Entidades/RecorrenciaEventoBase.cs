using Convoquei.Core.Eventos.Contratos;
using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Recorrencias.Entidades
{
    public abstract class RecorrenciaEventoBase : EntidadeBase, IEventoContrato
    {
        public string Nome { get; private set; }
        public string Local { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public TimeSpan FechamentoEscalaAntecedencia { get; private set; }
        public virtual Usuario Criador { get; private set; }
        public virtual Organizacao Organizacao { get; private set; }
        public DateTime? UltimaGeracao { get; private set; }

        protected RecorrenciaEventoBase() { }

        protected RecorrenciaEventoBase(string nome, string local, string descricao, DateTime dataHoraInicio, TimeSpan fechamentoEscalaAntecedencia, Usuario criador, Organizacao organizacao)
        {
            Nome = nome;
            Local = local;
            Descricao = descricao;
            DataHoraInicio = dataHoraInicio;
            FechamentoEscalaAntecedencia = fechamentoEscalaAntecedencia;
            Criador = criador;
            Organizacao = organizacao;
            UltimaGeracao = null;
        }

        public void ExecutarRecorrencia()
        {
            if (PrevisaoProximaGeracao > DateTime.UtcNow.Date)
                return;

            GerarEventos();

            UltimaGeracao = DateTime.UtcNow.Date;
        }

        public abstract DateTime PrevisaoProximaGeracao { get; }
        public abstract string DescricaoRecorrencia { get; }
        public abstract TipoEventoEnum Tipo { get; }
        protected abstract IEnumerable<Evento> GerarEventos();
    }
}
