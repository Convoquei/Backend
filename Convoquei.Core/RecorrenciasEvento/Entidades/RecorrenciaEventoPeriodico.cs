using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.RecorrenciasEvento.Entidades
{
    public class RecorrenciaEventoPeriodico : RecorrenciaEventoBase
    {
        public int IntervaloDias { get; private set; }
        public override TipoEventoEnum Tipo => TipoEventoEnum.IntervaloDias;
        public override string DescricaoRecorrencia => $"A cada {IntervaloDias} dias a partir de {DataHoraInicio:dd/MM/yyyy}";
        public override DateTime PrevisaoProximaGeracao { get 
            {
                int diasAntecedenciaCriarEventos = Organizacao.AntecedenciaDiasCriarEventosRecorrentes;

                DateTime projecaoProximaGeracao = UltimaGeracao.HasValue ?
                    UltimaGeracao.Value.AddDays(IntervaloDias).Date
                    : DataHoraInicio.AddDays(-diasAntecedenciaCriarEventos).Date;

                return projecaoProximaGeracao;
            } 
        }

        public RecorrenciaEventoPeriodico(string nome, string local, string descricao, DateTime dataHoraInicio, TimeSpan fechamentoEscalaAntecedencia, Usuario criador, Organizacao organizacao, int intervaloDias) : base(nome, local, descricao, dataHoraInicio, fechamentoEscalaAntecedencia, criador, organizacao)
        {
            if (intervaloDias < 3)
                throw new RegraDeNegocioExcecao("O intervalo de dias deve ser maior ou igual a 3 dias.");

            IntervaloDias = intervaloDias;

            ExecutarRecorrencia();
        }

        protected RecorrenciaEventoPeriodico()
        {
            
        }

        protected override IEnumerable<Evento> GerarEventos()
        {
            return Enumerable.Empty<Evento>();
        }
    }
}
