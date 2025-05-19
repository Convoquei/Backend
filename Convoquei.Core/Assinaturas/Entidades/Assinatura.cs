using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Organizacoes.Entidades;

namespace Convoquei.Core.Assinaturas.Entidades
{
    public sealed class Assinatura : EntidadeBase
    {
        public Plano Plano { get; private set; }
        public Organizacao Organizacao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }
        public bool Ativa => DataFim == null || DataFim > DateTime.UtcNow;

        public Assinatura(Plano plano, Organizacao organizacao, DateTime dataInicio, DateTime? dataFim)
        {
            Plano = plano;
            Organizacao = organizacao;
            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        private Assinatura()
        {
            
        }

        public static Assinatura CriarGratuita(Organizacao organizacao)
        {
            Plano plano = Plano.Gratuito();
            Assinatura assinatura = new(plano, organizacao, DateTime.UtcNow, null);
            return assinatura;
        }
    }
}
