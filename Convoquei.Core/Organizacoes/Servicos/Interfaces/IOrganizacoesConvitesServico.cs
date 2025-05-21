using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Servicos.Interfaces
{
    public interface IOrganizacoesConvitesServico
    {
        void ProcessarRespostaConvite(Organizacao organizacao, ConviteOrganizacao convite, Usuario usuario, bool aceito);
    }
}
