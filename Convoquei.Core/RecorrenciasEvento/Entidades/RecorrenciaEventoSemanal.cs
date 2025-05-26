using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.RecorrenciasEvento.Enumeradores;
using Convoquei.Core.RecorrenciasEvento.Extensoes;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.RecorrenciasEvento.Entidades
{
    public class RecorrenciaEventoSemanal : RecorrenciaEventoBase
    {
        public DiasEventoEnumFlag DiasRecorrenciaSemanaisFlag { get; private set; }
        public TimeSpan HorarioInicio { get; private set; }

        public RecorrenciaEventoSemanal(string nome, string local, string descricao, TimeSpan horarioInicio, TimeSpan fechamentoEscalaAntecedencia, Usuario criador, Organizacao organizacao, DiasEventoEnumFlag diasEventoEnumFlag) : base(nome, local, descricao, fechamentoEscalaAntecedencia, criador, organizacao)
        {
            if (diasEventoEnumFlag == DiasEventoEnumFlag.Nenhum)
                throw new RegraDeNegocioExcecao("É necessário informar ao menos um dia para criar uma recorrencia semanal.");

            DiasRecorrenciaSemanaisFlag = diasEventoEnumFlag;
            HorarioInicio = horarioInicio;
        }

        protected RecorrenciaEventoSemanal() { }

        public override TipoEventoEnum Tipo => TipoEventoEnum.Semanal;
        public override string DescricaoRecorrencia => GerarDiasSemanaFormatado();

        public override IEnumerable<Evento> GerarEventos()
        {
            int diasAntecedencia = Organizacao.AntecedenciaDiasCriarEventosRecorrentes;
            DateTime hoje = DateTime.UtcNow;
            DateTime dataFinal = hoje.AddDays(diasAntecedencia);

            HashSet<DateTime> datasEventosExistentes = ObterDatasEventosExistentes();

            IList<Evento> eventos = new List<Evento>();

            foreach(DateTime data in GerarProjecoesSemanais(hoje, dataFinal, DiasRecorrenciaSemanaisFlag, HorarioInicio))
            {
                if(!datasEventosExistentes.Contains(data))
                {
                    eventos.Add(new Evento(this, data));
                }
            }

            return eventos;
        }

        private static IEnumerable<DateTime> GerarProjecoesSemanais(DateTime inicio, DateTime limite, DiasEventoEnumFlag dias, TimeSpan horario)
        {
            IEnumerable<DayOfWeek> diasSemana = dias.ObterDiasSemana();

            DateTime data = inicio.Date;

            while (data <= limite)
            {
                if (diasSemana.Contains(data.DayOfWeek))
                {
                    yield return data.Add(horario);
                }

                data = data.AddDays(1);
            }
        }

        private string GerarDiasSemanaFormatado()
        {
            IList<string> dias = DiasRecorrenciaSemanaisFlag.ObterDiasSemana()
                .Select(d => d.ToString())
                .ToList();

            if (dias.Count == 1)
                return dias.First();

            string ultimoDia = dias.Last();
            dias.RemoveAt(dias.Count - 1);

            return $"{string.Join(", ", dias)} e {ultimoDia}";
        }

    }
}
