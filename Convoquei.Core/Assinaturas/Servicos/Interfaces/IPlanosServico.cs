using Convoquei.Core.Assinaturas.Entidades;

namespace Convoquei.Core.Assinaturas.Servicos.Interfaces
{
    public interface IPlanosServico
    {
        Task<Plano> GerarOuObterPlanoGratuitoAsync(CancellationToken cancellationToken);
    }
}
