using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Enumeradores.CargoMembroEnum;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.Entidades
{
    public sealed class ParticipacaoEvento : EntidadeBase
    {
        public Usuario Usuario { get; private set; }
        public CargoMembroEnum CargoNaEpoca { get; private set; }
        public StatusParticipacaoEventoEnum Status { get; private set; }

        public ParticipacaoEvento(Usuario usuario, CargoMembroEnum cargoNaEpoca) : base(Guid.NewGuid())
        {
            Usuario = usuario;
            CargoNaEpoca = cargoNaEpoca;
            Status = StatusParticipacaoEventoEnum.NaoInformado;
        }
    }
}
