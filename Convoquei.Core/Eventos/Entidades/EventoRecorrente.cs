using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Entidades;

namespace Convoquei.Core.Eventos.Entidades
{
    public sealed class EventoRecorrente : EventoBase
    {
        public DiasDaSemanaEnum DiasRecorrencia { get; private set; }
        public override TipoEventoEnum Tipo => TipoEventoEnum.Recorrente;

        public EventoRecorrente(
            DiasDaSemanaEnum diasRecorrencia,
            string nome,
            CategoriaEventoEnum categoria,
            string local,
            string descricao,
            DateTime dataHora,
            Organizacao organizacao,
            MembroOrganizacao membroCriador,
            int minutosAntesParaFechamentoAutomatico) : base(nome, categoria, local, descricao, dataHora, organizacao, membroCriador, minutosAntesParaFechamentoAutomatico)
        {
            if(diasRecorrencia == DiasDaSemanaEnum.Nenhum)
                throw new RegraDeNegocioExcecao("Pelo menos um dia da semana deve ser selecionado para recorrencia.");

            DiasRecorrencia = diasRecorrencia;
        }

        public void EditarRecorrencia(DiasDaSemanaEnum diasRecorrencia)
        {
            if (diasRecorrencia == DiasDaSemanaEnum.Nenhum)
                throw new RegraDeNegocioExcecao("Pelo menos um dia da semana deve ser selecionado para recorrencia.");

            DiasRecorrencia = diasRecorrencia;
        }

        public bool AconteceNoDiaEspecifico(DateTime data)
        {
            var diaEnum = (DiasDaSemanaEnum)(1 << (int)data.DayOfWeek);

            return DiasRecorrencia.HasFlag(diaEnum);
        }
    }
}
