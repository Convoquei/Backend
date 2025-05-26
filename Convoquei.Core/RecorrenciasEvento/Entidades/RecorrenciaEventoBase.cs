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
        public TimeSpan FechamentoEscalaAntecedencia { get; private set; }
        public virtual Usuario Criador { get; private set; }
        public virtual Organizacao Organizacao { get; private set; }
        public DateTime? DataUltimoEventoGerado { get; private set; }
        public DateTime? PrevisaoProximaGeracao { get; protected set; }

        protected RecorrenciaEventoBase() { }

        protected RecorrenciaEventoBase(string nome, string local, string descricao, TimeSpan fechamentoEscalaAntecedencia, Usuario criador, Organizacao organizacao)
        {
            Nome = nome;
            Local = local;
            Descricao = descricao;
            FechamentoEscalaAntecedencia = fechamentoEscalaAntecedencia;
            Criador = criador;
            Organizacao = organizacao;
            DataUltimoEventoGerado = null;
        }

        protected HashSet<DateTime> ObterDatasEventosExistentes()
        {
            HashSet<DateTime> datasEventosExistentes = Organizacao.Eventos
                .Where(e => e.Recorrencia == this)
                .Select(e => e.DataHoraInicio)
                .ToHashSet();

            return datasEventosExistentes;
        }

        public abstract string DescricaoRecorrencia { get; }
        public abstract TipoEventoEnum Tipo { get; }
        public abstract IEnumerable<Evento> GerarEventos();
    }
}
