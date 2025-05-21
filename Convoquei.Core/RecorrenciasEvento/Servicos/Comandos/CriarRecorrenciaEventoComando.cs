using Convoquei.Core.Eventos.Contratos;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.RecorrenciasEvento.Enumeradores;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.RecorrenciasEvento.Servicos.Comandos
{
    public record CriarRecorrenciaEventoComando(
        string Nome,
        string Local,
        string Descricao,
        DateTime DataHoraInicio,
        TimeSpan FechamentoEscalaAntecedencia,
        Usuario Criador,
        Organizacao Organizacao,
        TipoEventoEnum Tipo,
        int? IntervaloDias,
        DiasEventoEnumFlag? DiasSemanaBitmap
    ) : IEventoContrato;
}
