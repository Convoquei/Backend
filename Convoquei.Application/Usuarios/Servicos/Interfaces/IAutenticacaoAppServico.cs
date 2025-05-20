using Convoquei.DataTransfer.Usuarios.Request;
using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.Application.Usuarios.Servicos.Interfaces
{
    public interface IAutenticacaoAppServico
    {
        Task<UsuarioResponse> CadastrarAsync(CadastrarRequest request, CancellationToken cancellationToken);
        Task<AutenticadoResponse> AutenticarAsync(LogarRequest request, CancellationToken cancellationToken);
    }
}
