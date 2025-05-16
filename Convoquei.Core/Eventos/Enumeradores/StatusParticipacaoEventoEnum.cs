using System.ComponentModel;

namespace Convoquei.Core.Eventos.Enumeradores
{
    public enum StatusParticipacaoEventoEnum
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Disponível")]
        Disponivel = 1,

        [Description("Escalado")]
        Escalado = 2,

        [Description("Não escalado")]
        NaoEscalado = 3,

        [Description("Indisponível")]
        Indisponivel = 4
    }
}
