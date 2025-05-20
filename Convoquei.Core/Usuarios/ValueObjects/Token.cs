namespace Convoquei.Core.Usuarios.ValueObjects
{
    public record Token
    {
        public string Acesso { get; init; }
        public string Refresh { get; init; }

        public Token(string acesso, string refresh)
        {
            Acesso = acesso;
            Refresh = refresh;
        }
    }
}
