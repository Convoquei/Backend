using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Servicos.Comandos;

namespace Convoquei.Core.Eventos.Servicos.Interfaces;

public interface IEventosServico
{
    Evento CriarEvento(CriarEventoComando comando, CancellationToken cancellationToken);
}