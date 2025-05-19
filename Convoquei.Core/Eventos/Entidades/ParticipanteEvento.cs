using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Genericos.Extensoes;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.Entidades
{
    public sealed class ParticipanteEvento : EntidadeBase
    {
        public Usuario Usuario { get; private set; }
        public Evento Evento { get; private set; }
        public StatusParticipacaoEventoEnum StatusParticipacao { get; private set; }

        public ParticipanteEvento(Usuario usuario, Evento evento, StatusParticipacaoEventoEnum statusParticipacao)
        {
            Usuario = usuario;
            Evento = evento;
            StatusParticipacao = statusParticipacao;
        }

        private ParticipanteEvento()
        {
            
        }

        public void SetStatusParticipacao(MembroOrganizacao membroModificador, StatusParticipacaoEventoEnum novoStatus)
        {
            if (novoStatus == StatusParticipacaoEventoEnum.NaoInformado)
                throw new RegraDeNegocioExcecao("O status da participação não pode ser definido como não informado.");

            bool proprioUsuarioModificando = membroModificador.Usuario.Equals(Usuario);

            if (StatusParticipacao == StatusParticipacaoEventoEnum.NaoInformado && !proprioUsuarioModificando)
                throw new RegraDeNegocioExcecao("Somente o próprio participante pode definir o status inicial da participação.");

            if (EhStatusDoTipoAutoGerenciado(novoStatus) && !proprioUsuarioModificando)
                throw new RegraDeNegocioExcecao($"Somente o próprio participante pode alterar o status para {novoStatus.GetDescription().ToLower()}.");

            if (EhStatusDoTipoAdministrativo(novoStatus))
            {
                if (!membroModificador.PossuiPermissoesAdministrativas(Evento.Organizacao))
                    throw new RegraDeNegocioExcecao("Somente administradores da organização podem alterar esse status.");

                if (!TransicaoEntreEscaladosValida(StatusParticipacao, novoStatus))
                {
                    throw new RegraDeNegocioExcecao(
                        $"O status da participação só pode ser definido como '{novoStatus.GetDescription().ToLowerInvariant()}' se o participante estiver como 'Disponível', 'Escalado' ou 'Não Escalado'.");
                }
            }

            StatusParticipacao = novoStatus;
        }


        private static bool EhStatusDoTipoAutoGerenciado(StatusParticipacaoEventoEnum status) =>
            status is StatusParticipacaoEventoEnum.Disponivel or StatusParticipacaoEventoEnum.Indisponivel;

        private static bool EhStatusDoTipoAdministrativo(StatusParticipacaoEventoEnum status) =>
            status is StatusParticipacaoEventoEnum.Escalado or StatusParticipacaoEventoEnum.NaoEscalado;

        private static bool TransicaoEntreEscaladosValida(StatusParticipacaoEventoEnum atual, StatusParticipacaoEventoEnum novo)
        {
            if ((atual, novo) is
                (StatusParticipacaoEventoEnum.Escalado, StatusParticipacaoEventoEnum.NaoEscalado) or
                (StatusParticipacaoEventoEnum.NaoEscalado, StatusParticipacaoEventoEnum.Escalado))
                return true;

            return atual == StatusParticipacaoEventoEnum.Disponivel;
        }

    }
}
