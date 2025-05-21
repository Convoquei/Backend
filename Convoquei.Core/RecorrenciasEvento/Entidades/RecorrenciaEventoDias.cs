using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Recorrencias.Entidades;

namespace Convoquei.Core.RecorrenciasEvento.Entidades
{
    public class RecorrenciaEventoDias : RecorrenciaEventoBase
    {
        public int IntervaloDias { get; private set; }

        public override IEnumerable<Evento> GerarEventos()
        {
            throw new NotImplementedException();
        }
    }
}
