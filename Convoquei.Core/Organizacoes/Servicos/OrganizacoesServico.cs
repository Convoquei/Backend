using Convoquei.Core.Assinaturas.Entidades;
using Convoquei.Core.Assinaturas.Servicos.Interfaces;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Servicos.Interfaces;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Servicos
{
    public class OrganizacoesServico : IOrganizacoesServico
    {
        private readonly IPlanosServico _planosServico;

        public OrganizacoesServico(IPlanosServico planosServico)
        {
            _planosServico = planosServico;
        }

        public async Task<Organizacao> CriarAsync(string nome, Usuario criador, CancellationToken cancellationToken)
        {
            Plano planoGratuito = await _planosServico.GerarOuObterPlanoGratuitoAsync(cancellationToken);

            Organizacao organizacao = new(nome, planoGratuito, criador);

            return organizacao;
        }
    }
}
