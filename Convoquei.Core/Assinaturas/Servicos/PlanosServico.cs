using Convoquei.Core.Assinaturas.Entidades;
using Convoquei.Core.Assinaturas.Enumeradores;
using Convoquei.Core.Assinaturas.Repositorios;
using Convoquei.Core.Assinaturas.Servicos.Interfaces;

namespace Convoquei.Core.Assinaturas.Servicos
{
    public class PlanosServico : IPlanosServico
    {
        private readonly IPlanosRepositorio _planosRepositorio;

        public PlanosServico(IPlanosRepositorio planosRepositorio)
        {
            _planosRepositorio = planosRepositorio;
        }

        public async Task<Plano> GerarOuObterPlanoGratuitoAsync(CancellationToken cancellationToken)
        {
            Plano? planoGratuito = await _planosRepositorio.RecuperarAsync(plano => plano.Tipo == TipoPlanoEnum.Gratuito, cancellationToken);
            if (planoGratuito is null)
            {
                planoGratuito = new("Plano Gratuito", 10, 7, 0, TipoPlanoEnum.Gratuito);

                await _planosRepositorio.InserirAsync(planoGratuito, cancellationToken);
            }
            return planoGratuito;
        }
    }
}
