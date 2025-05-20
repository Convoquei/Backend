
using Convoquei.Api.Responses;
using Convoquei.Application.Autenticacao;
using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.Repositorios;
using System.Security.Claims;

namespace Convoquei.Api.Middlewares
{
    public class AutenticacaoMiddleware
    {
        private readonly RequestDelegate _next;

        public AutenticacaoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUsuariosRepositorio usuariosRepositorio, IUsuarioSessao usuarioSessao)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

            bool autenticado = context.User.Identity?.IsAuthenticated ?? false;
            if (!autenticado || string.IsNullOrWhiteSpace(token))
            {
                await TokenJwtInvalido(context);
                return;
            }

            var id = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if(id is null || !Guid.TryParse(id.Value, out Guid idUsuario))
            {
                await TokenJwtInvalido(context);
                return;
            }

            Usuario? usuario = await usuariosRepositorio.RecuperarAsync(idUsuario, context.RequestAborted);
            if(usuario?.Token?.Acesso != token || usuario is not Usuario)
            {
                await TokenJwtInvalido(context);
                return;
            }

            usuarioSessao.Definir(usuario);

            await _next(context);
        }

        private async Task TokenJwtInvalido(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(ApiResponse<object>.Falha("Token invalido."));
        }
    }
}
