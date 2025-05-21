using Convoquei.Core.Genericos.Entidades;

namespace Convoquei.Core.Genericos.Excecoes
{
    public class EntidadeNaoEncontradaExcecao<T> : Exception where T : EntidadeBase
    {
        private const string mensagem = $"Entidade {nameof(T)} não encontrada.";

        public EntidadeNaoEncontradaExcecao() : base(mensagem)
        {
            
        }
    }
}
