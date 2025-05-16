using Convoquei.Core.Genericos.Excecoes;
using System.Text.RegularExpressions;

namespace Convoquei.Core.Genericos.ValueObjects
{
    public record Email
    {
        public string Valor { get; }

        public Email(string valor)
        {
            if(string.IsNullOrWhiteSpace(valor))
                throw new AtributoInvalidoExcecao("Email não pode ser vazio ou nulo!");

            if (!Regex.IsMatch(valor, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new AtributoInvalidoExcecao("Formato de email invalido, utilize email@dominio.com!");

            Valor = valor.ToLowerInvariant();
        }

        public override string ToString()
            => Valor;

        public static implicit operator string(Email email) => email.Valor;
        public static explicit operator Email(string email) => new Email(email);
    }
}
