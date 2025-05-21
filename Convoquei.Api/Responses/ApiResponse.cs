using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Convoquei.Api.Responses
{
    public class ApiResponse<T>
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public T? Dados { get; set; }
        public IList<string> Erros { get; set; } = [];

        public static ApiResponse<T> Ok(T dados, string? mensagem = null)
            => new() { Sucesso = true, Dados = dados, Mensagem = mensagem };

        public static ApiResponse<T> Ok(string mensagem)
            => new() { Sucesso = true, Mensagem = mensagem };

        public static ApiResponse<T> Falha(string mensagem)
            => new() { Sucesso = false, Mensagem = mensagem };

        public static ApiResponse<T> Falha(ModelStateDictionary modelState, string mensagem = "Ocorreram um ou mais erros de validação.")
            => new()
            {
                Sucesso = false,
                Mensagem = mensagem,
                Erros = modelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .SelectMany(ms => ms.Value.Errors.Select(e =>
                        string.IsNullOrEmpty(ms.Key)
                            ? e.ErrorMessage
                            : $"{ms.Key}: {e.ErrorMessage}"
                    ))
                    .ToList()
            };
    }
}
