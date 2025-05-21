using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.RecorrenciasEvento.Enumeradores;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.RecorrenciasEvento.Entidades
{
    public class RecorrenciaEventoSemanal : RecorrenciaEventoBase
    {
        public DiasEventoEnumFlag DiasRecorrenciaSemanaisFlag { get; private set; }
        public override DateTime PrevisaoProximaGeracao { get 
            { 
                int diasAntecedenciaCriarEventos = Organizacao.AntecedenciaDiasCriarEventosRecorrentes;

                DateTime projecaoProximaData = UltimaGeracao.HasValue ?
                    UltimaGeracao.Value.AddDays(diasAntecedenciaCriarEventos).Date
                    : DataHoraInicio.AddDays(-diasAntecedenciaCriarEventos).Date;

                return projecaoProximaData;
            } 
        }

        protected RecorrenciaEventoSemanal()
        {

        }

        public RecorrenciaEventoSemanal(string nome, string local, string descricao, DateTime dataHoraInicio, TimeSpan fechamentoEscalaAntecedencia, Usuario criador, Organizacao organizacao, DiasEventoEnumFlag diasEventoEnumFlag) : base(nome, local, descricao, dataHoraInicio, fechamentoEscalaAntecedencia, criador, organizacao)
        {
            if (diasEventoEnumFlag == DiasEventoEnumFlag.Nenhum)
                throw new RegraDeNegocioExcecao("É necessário informar ao menos um dia para criar uma recorrencia semanal.");

            DiasRecorrenciaSemanaisFlag = diasEventoEnumFlag;

            ExecutarRecorrencia();
        }

        public override TipoEventoEnum Tipo => TipoEventoEnum.Semanal;
        public override string DescricaoRecorrencia => $"Todo {GerarDiasSemanaFormatado()}";

        protected override IEnumerable<Evento> GerarEventos()
        {
            return Enumerable.Empty<Evento>();
        }

        private string GerarDiasSemanaFormatado()
        {
            var dias = DiasRecorrenciaSemanaisFlag.ToString().Split(",").Select(dia => dia.Trim()).ToList();
            if (dias.Count == 1)
                return dias.First();
            var ultimoDia = dias.Last();
            dias.RemoveAt(dias.Count - 1);
            return $"{string.Join(", ", dias)} e {ultimoDia}";
        }
    }
}
