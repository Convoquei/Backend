using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;

namespace Convoquei.Core.Genericos.Extensoes
{
    public static class EntidadeBaseExtensao
    {
        public static void LancarRegraDeNegocioExcecaoSe<TEntidade>(this TEntidade entidade, Func<TEntidade, bool> condicao, string mensagem) where TEntidade : EntidadeBase
        {
            if (condicao(entidade))
                throw new RegraDeNegocioExcecao(mensagem);
        }
    }
}
