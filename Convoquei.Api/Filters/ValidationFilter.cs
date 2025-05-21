using Convoquei.Api.Responses;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var erros = context.ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .SelectMany(ms => ms.Value.Errors.Select(e =>
                    string.IsNullOrEmpty(ms.Key)
                        ? e.ErrorMessage
                        : $"{ms.Key}: {e.ErrorMessage}"
                ))
                .ToList();

            var resposta = ApiResponse<object>.Falha(context.ModelState);

            context.Result = new BadRequestObjectResult(resposta);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
