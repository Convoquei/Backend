using Microsoft.Extensions.Logging;

namespace Convoquei.Application.Genericos.Extensoes
{
    public static class LoggerExtensoes
    {
        public static void LogError(this ILogger logger, Exception exception, string nomeOperacao, object? payload)
        {
            logger.LogError(
                exception,
                "Erro ao processar {Operacao}. Detalhes: {Mensagem}. Payload: {@payload}",
                nomeOperacao,
                exception.Message,
                payload
            );
        }
    }
}
