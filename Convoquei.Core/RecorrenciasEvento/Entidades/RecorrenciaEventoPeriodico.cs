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
        public DateTime PrimeiraOcorrencia { get; private set; }
        public override TipoEventoEnum Tipo => TipoEventoEnum.IntervaloDias;
        public override string DescricaoRecorrencia => $"A cada {IntervaloDias} dias a partir de {PrimeiraOcorrencia:dd/MM/yyyy}";

        protected RecorrenciaEventoPeriodico() { }

        public RecorrenciaEventoPeriodico(
            string nome,
            string local,
            string descricao,
            DateTime dataPrimeiraOcorrencia,
            TimeSpan fechamentoEscalaAntecedencia,
            Usuario criador,
            Organizacao organizacao,
            int intervaloDias
        ) : base(nome, local, descricao, fechamentoEscalaAntecedencia, criador, organizacao)
        {
            if (intervaloDias < 3)
                throw new RegraDeNegocioExcecao("O intervalo de dias deve ser maior ou igual a 3 dias.");
            if (dataPrimeiraOcorrencia <= DateTime.UtcNow.AddHours(6))
                throw new RegraDeNegocioExcecao("A primeira ocorrência do evento deve ter pelo menos 6 horas de antecedência.");

            IntervaloDias = intervaloDias;
            PrimeiraOcorrencia = dataPrimeiraOcorrencia;
            PrevisaoProximaGeracao = PrimeiraOcorrencia.AddDays(-Organizacao.AntecedenciaDiasCriarEventosRecorrentes);
        }

        public override IEnumerable<Evento> GerarEventos()
        {
            int diasAntecedencia = Organizacao.AntecedenciaDiasCriarEventosRecorrentes;
            DateTime hoje = DateTime.UtcNow;

            DateTime proximaOcorrenciaInicial = ObterProximaOcorrenciaAPartirDeHoje(PrimeiraOcorrencia, IntervaloDias, hoje);
            DateTime dataFinal = hoje.AddDays(diasAntecedencia);

            HashSet<DateTime> datasEventosExistentes = ObterDatasEventosExistentes();

            var eventos = new List<Evento>();

            DateTime dataCorrente = proximaOcorrenciaInicial;

            while (dataCorrente <= dataFinal)
            {
                if (!datasEventosExistentes.Contains(dataCorrente))
                {
                    eventos.Add(new Evento(this, dataCorrente));
                }

                dataCorrente = dataCorrente.AddDays(IntervaloDias);
            }

            return eventos;
        }

        private static DateTime ObterProximaOcorrenciaAPartirDeHoje(DateTime primeiraOcorrencia, int intervaloDias, DateTime hoje)
        {
            if (primeiraOcorrencia >= hoje)
                return primeiraOcorrencia;

            int diasDiferenca = (hoje.Date - primeiraOcorrencia.Date).Days;
            int saltos = (int)Math.Ceiling(diasDiferenca / (double)intervaloDias);

            return primeiraOcorrencia.AddDays(saltos * intervaloDias);
        }
    }

}
