using Convoquei.Core.Assinaturas.Entidades;
using Convoquei.Core.Assinaturas.Servicos.Interfaces;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Organizacoes.Repositorios;
using Convoquei.Core.Organizacoes.Servicos.Interfaces;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Servicos
{
    public class OrganizacoesServico : IOrganizacoesServico
    {
        private readonly IPlanosServico _planosServico;
        private readonly IOrganizacoesRepositorio _organizacoesRepositorio;

        public OrganizacoesServico(IPlanosServico planosServico, IOrganizacoesRepositorio organizacoesRepositorio)
        {
            _planosServico = planosServico;
            _organizacoesRepositorio = organizacoesRepositorio;
        }

        public async Task<Organizacao> CriarAsync(string nome, Usuario criador, CancellationToken cancellationToken)
        {
            Plano planoGratuito = await _planosServico.GerarOuObterPlanoGratuitoAsync(cancellationToken);

            Organizacao organizacao = new(nome, planoGratuito, criador);

            return organizacao;
        }

        public async Task<Organizacao> ValidarAsync(Guid id, CancellationToken cancellationToken)
        {
            Organizacao? organizacao = await _organizacoesRepositorio.RecuperarAsync(id, cancellationToken);
            if (organizacao is null)
                throw new EntidadeNaoEncontradaExcecao($"Não foi encontrada nenhuma organização!");

            return organizacao;
        }
    }
}
