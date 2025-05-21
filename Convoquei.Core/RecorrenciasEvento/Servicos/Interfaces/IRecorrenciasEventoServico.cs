using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.RecorrenciasEvento.Servicos.Comandos;

namespace Convoquei.Core.RecorrenciasEvento.Servicos.Interfaces
{
    public interface IRecorrenciasEventoServico
    {
        RecorrenciaEventoBase CriarRecorrencia(CriarRecorrenciaEventoComando comando);
    }
}
