using Convoquei.Api.Responses;
using Convoquei.Core.Genericos.Excecoes;
using System.Text.Json;

namespace Convoquei.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var mensagem = "Ocorreu um erro interno no servidor.";
            var detalheErro = _env.IsDevelopment() ? exception.ToString() : null;

            switch (exception)
            {
                case RegraDeNegocioExcecao:
                case AtributoInvalidoExcecao:
                case EntidadeNaoEncontradaExcecao:
                    statusCode = StatusCodes.Status400BadRequest;
                    mensagem = exception.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    mensagem = "Acesso não autorizado.";
                    break;

                default:
                    mensagem = _env.IsDevelopment()
                        ? $"{exception.Message} - {exception.StackTrace}"
                        : "Erro inesperado, tente novamente mais tarde.";
                    break;
            }

            var response = new ApiResponse<string>
            {
                Sucesso = false,
                Mensagem = mensagem,
                Dados = detalheErro
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
