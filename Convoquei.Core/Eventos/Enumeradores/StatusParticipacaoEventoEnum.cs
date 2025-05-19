using System.ComponentModel;

namespace Convoquei.Core.Eventos.Enumeradores
{
    public enum StatusParticipacaoEventoEnum
    {
        [Description("Não informado")]
        NaoInformado = 1,
        [Description("Disponível")]
        Disponivel = 2,
        [Description("Indisponível")]
        Indisponivel = 3,
        [Description("Escalado")]
        Escalado = 4,
        [Description("Não escalado")]
        NaoEscalado = 5
    }
}
