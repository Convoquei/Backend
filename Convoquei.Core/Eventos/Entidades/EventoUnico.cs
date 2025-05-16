using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Organizacoes.Entidades;

namespace Convoquei.Core.Eventos.Entidades
{
    public sealed class EventoUnico : EventoBase
    {
        public override TipoEventoEnum Tipo => TipoEventoEnum.Unico;

        public EventoUnico(
            string nome,
            CategoriaEventoEnum categoria,
            string local,
            string descricao,
            DateTime dataHora,
            Organizacao organizacao,
            Membro criador,
            int minutosAntesParaFechamentoAutomatico) : base(nome, categoria, local, descricao, dataHora, organizacao, criador, minutosAntesParaFechamentoAutomatico)
        {
        }
    }
}
