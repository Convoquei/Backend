using System.Data;
using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Servicos.Interfaces;
using Convoquei.Core.Eventos.Servicos.Comandos;

namespace Convoquei.Core.Eventos.Servicos;

public class EventosServico : IEventosServico
{
    public Evento CriarEvento(CriarEventoComando comando, CancellationToken cancellationToken)
    {
        Evento evento = new(
            comando.Nome,
            comando.Local,
            comando.Descricao,
            comando.Tipo,
            comando.DataHoraInicio,
            comando.FechamentoEscalaAntecedencia,
            comando.Criador,
            comando.Organizacao
        );
        
        comando.Organizacao.AdicionarEvento(comando.Criador, evento);
        
        return evento;
    }
}