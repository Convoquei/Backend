using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.RecorrenciasEvento.Enumeradores;

namespace Convoquei.Core.RecorrenciasEvento.Entidades
{
    public class RecorrenciaEventoSemanal : RecorrenciaEventoBase
    {
        public DiasEventoEnumFlag DiasRecorrenciaSemanaisFlag { get; private set; }

        protected RecorrenciaEventoSemanal()
        {
            
        }

        public override IEnumerable<Evento> GerarEventos()
        {
            throw new NotImplementedException();
        }
    }
}
