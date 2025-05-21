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
        public DateTime ProximaGeracao { get; private set; }

        protected RecorrenciaEventoBase() { }

        public abstract IEnumerable<Evento> GerarEventos();
    }
}
