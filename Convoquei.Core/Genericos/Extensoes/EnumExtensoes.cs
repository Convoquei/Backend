using System.ComponentModel;

namespace Convoquei.Core.Genericos.Extensoes
{
    public static class EnumExtensoes
    {
        public static string GetDescription(this Enum valor)
        {
            var campo = valor.GetType().GetField(valor.ToString());

            if (campo == null)
                return valor.ToString();

            var atributo = campo
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;

            return atributo?.Description ?? valor.ToString();
        }

    }
}
