using Convoquei.Core.Genericos.Extensoes;

namespace Convoquei.DataTransfer.Comuns.Responses
{
    public class GenericoEnumResponse<TValor> where TValor : struct
    {
        public TValor Valor { get; }
        public string Descricao { get; }

        public GenericoEnumResponse(Enum enumerador)
        {
            Valor = (TValor)Convert.ChangeType(enumerador, typeof(TValor));
            Descricao = enumerador.GetDescription();
        }

        public static implicit operator GenericoEnumResponse<TValor>(Enum enumerador)
        {
            return new GenericoEnumResponse<TValor>(enumerador);
        }
    }
}
