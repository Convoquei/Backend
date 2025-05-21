using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Servicos.Interfaces;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Servicos
{
    public class OrganizacoesConvitesServico : IOrganizacoesConvitesServico
    {
        public void ProcessarRespostaConvite(Organizacao organizacao, ConviteOrganizacao convite, Usuario usuario, bool aceito)
        {
            if(aceito)
            {
                AceitarConvite(organizacao, convite, usuario);
                return;
            }

            RecusarConvite(organizacao, convite, usuario);
        }

        private static void AceitarConvite(Organizacao organizacao, ConviteOrganizacao convite, Usuario usuario)
        {
            MembroOrganizacao membro = convite.Aceitar(usuario);

            organizacao.AdicionarMembro(membro);

            organizacao.Convites.Remove(convite);
        }

        private static void RecusarConvite(Organizacao organizacao, ConviteOrganizacao convite, Usuario usuario)
        {
            convite.Negar(usuario);

            organizacao.Convites.Remove(convite);
        }
    }
}
