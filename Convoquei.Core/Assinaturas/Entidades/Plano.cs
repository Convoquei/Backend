using Convoquei.Core.Assinaturas.Enumeradores;
using Convoquei.Core.Genericos.Entidades;

namespace Convoquei.Core.Assinaturas.Entidades
{
    public sealed class Plano : EntidadeBase
    {
        public string Nome { get; private set; }
        public int LimiteMembros { get; private set; }
        public int LimiteEventosMensais { get; private set; }
        public decimal Valor { get; private set; }
        public TipoPlanoEnum Tipo { get; private set; }

        public HashSet<Assinatura> _assinaturas { get; private set; } = new();
        public IReadOnlyCollection<Assinatura> Assinaturas => _assinaturas;

        public Plano(string nome, int limiteMembros, int limiteEventosMensais, decimal valor, TipoPlanoEnum tipo) : base()
        {
            Nome = nome;
            LimiteMembros = limiteMembros;
            LimiteEventosMensais = limiteEventosMensais;
            Valor = valor;
            Tipo = tipo;
        }

        private Plano()
        {
            
        }

        public static Plano Gratuito()
            => new Plano(nome: nameof(TipoPlanoEnum.Gratuito), limiteMembros: 10, limiteEventosMensais: 8, valor: 0, tipo: TipoPlanoEnum.Gratuito);
    }
}
