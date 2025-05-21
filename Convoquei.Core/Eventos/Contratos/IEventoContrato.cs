using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.Contratos
{
    public interface IEventoContrato
    {
        string Nome { get; }
        string Local { get; }
        string Descricao { get; }
        DateTime DataHoraInicio { get; }
        TimeSpan FechamentoEscalaAntecedencia { get; }
        Usuario Criador { get; }
        Organizacao Organizacao { get; }
        TipoEventoEnum Tipo { get; }
    }
}
