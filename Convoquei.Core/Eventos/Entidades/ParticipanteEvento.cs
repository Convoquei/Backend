using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Enumeradores.CargoMembroEnum;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.Entidades
{
    public sealed class ParticipanteEvento : EntidadeBase
    {
        public Usuario Usuario { get; private set; }
        public EventoBase Evento { get; private set; }
        public CargoMembroEnum CargoNaEpoca { get; private set; }
        public StatusParticipacaoEventoEnum Status { get; private set; }

        public ParticipanteEvento(Usuario usuario, EventoBase evento, CargoMembroEnum cargoNaEpoca) : base(Guid.NewGuid())
        {
            Usuario = usuario;
            CargoNaEpoca = cargoNaEpoca;
            Evento = evento;
            Status = StatusParticipacaoEventoEnum.NaoInformado;
        }

        public void AlterarStatus(StatusParticipacaoEventoEnum novoStatus)
        {
            switch (novoStatus)
            {
                case StatusParticipacaoEventoEnum.Disponivel:
                    Disponibilizar();
                    break;
                case StatusParticipacaoEventoEnum.Indisponivel:
                    Indisponibilizar();
                    break;
                case StatusParticipacaoEventoEnum.Escalado:
                    Escalar();
                    break;
                case StatusParticipacaoEventoEnum.NaoEscalado:
                    Desescalar();
                    break;
                case StatusParticipacaoEventoEnum.NaoInformado:
                    throw new RegraDeNegocioExcecao("Não é possível alterar o status para Não Informado.");
                default:
                    return;
            }
        }

        private void Escalar()
        {
            if (Status != StatusParticipacaoEventoEnum.Disponivel &&
                Status != StatusParticipacaoEventoEnum.NaoEscalado)
            {
                throw new RegraDeNegocioExcecao("Escalação permitida somente quando disponível ou não escalado.");
            }

            Status = StatusParticipacaoEventoEnum.Escalado;
        }

        private void Desescalar()
        {
            if (Status != StatusParticipacaoEventoEnum.Escalado &&
                Status != StatusParticipacaoEventoEnum.Disponivel)
            {
                throw new RegraDeNegocioExcecao("Desescalação permitida somente quando escalado ou disponível.");
            }

            Status = StatusParticipacaoEventoEnum.NaoEscalado;
        }

        private void Disponibilizar()
        {
            if (Evento.StatusEscala != StatusEscalaEventoEnum.Aberta)
                throw new RegraDeNegocioExcecao("O evento está fechado para disponibilidades.");

            if (Status != StatusParticipacaoEventoEnum.NaoInformado &&
                Status != StatusParticipacaoEventoEnum.Indisponivel)
            {
                throw new RegraDeNegocioExcecao("Disponibilidade permitida somente quando não informado ou indisponível.");
            }

            Status = StatusParticipacaoEventoEnum.Disponivel;
        }

        private void Indisponibilizar()
        {
            if (Evento.StatusEscala != StatusEscalaEventoEnum.Aberta)
                throw new RegraDeNegocioExcecao("O evento está fechado para disponibilidades.");

            if (Status != StatusParticipacaoEventoEnum.Disponivel &&
                Status != StatusParticipacaoEventoEnum.NaoInformado)
            {
                throw new RegraDeNegocioExcecao("Indisponibilidade permitida somente quando disponível ou não informado.");
            }
            Status = StatusParticipacaoEventoEnum.Indisponivel;
        }
    }
}
