using Convoquei.Core.Genericos.Excecoes;

namespace Convoquei.Core.Usuarios.ValueObjects
{
    public record Email
    {
        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new AtributoInvalidoExcecao("O endereço de e-mail não pode ser nulo ou vazio.");
            if (!IsValidEmail(endereco))
                throw new AtributoInvalidoExcecao("O endereço de e-mail fornecido não é válido.");

            Endereco = endereco.ToLowerInvariant();
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static implicit operator string(Email email)
        {
            return email.Endereco;
        }

        public static implicit operator Email(string email)
        {
            return new Email(email);
        }
    }
}
