using System.Linq.Expressions;
using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Repositorios;
using Convoquei.Infra.Data;
using Convoquei.Infra.Genericos.Repositorios;

namespace Convoquei.Infra.Eventos.Repositorios;

public class EventosRepositorio : RepositorioGenerico<Evento>, IEventosRepositorio
{
    public EventosRepositorio(AppDbContext context) : base(context)
    {
    }
}