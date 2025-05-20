using System.ComponentModel.DataAnnotations;

namespace Convoquei.DataTransfer.Usuarios.Request
{
    public record LogarRequest(
        [EmailAddress] 
        string Email,
        string Senha
    );
}
