using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Extensoes;
using System.ComponentModel.DataAnnotations;

namespace Convoquei.DataTransfer.RecorrenciasEvento.Requests
{
    public record CriarRecorrenciaRequest(
        string Nome,
        string Local,
        string Descricao,
        DateTime DataHoraInicio,
        int HorasFechamentoEscalaAntecedencia,
        int TipoEvento,
        int? IntervaloDias,
        int? DiasSemanaBitmap
    ) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var tipoEventoValido = Enum.IsDefined(typeof(TipoEventoEnum), TipoEvento);

            if (!tipoEventoValido)
            {
                yield return new ValidationResult(
                    $"Tipo de evento inválido. Valores permitidos: {ObterTiposPermitidos()}",
                    new[] { nameof(TipoEvento) }
                );
            }
        }

        private static string ObterTiposPermitidos()
        {
            return string.Join(", ", Enum.GetValues<TipoEventoEnum>()
                .Select(e => $"{(int)e} - {e.GetDescription()}"));
        }
    }
}
