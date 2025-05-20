using System.ComponentModel.DataAnnotations;

namespace Convoquei.DataTransfer.Usuarios.Request
{
    public record CadastrarRequest(
        string Nome,
        [EmailAddress]
        string Email,
        string Senha
    );
}
