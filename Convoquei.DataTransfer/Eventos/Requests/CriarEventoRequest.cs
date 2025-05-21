using System.ComponentModel.DataAnnotations;
using Convoquei.Core.Eventos.Enumeradores;

namespace Convoquei.DataTransfer.Eventos.Requests;

public record CriarEventoRequest(
    string Nome,
    string Local,
    string Descricao,
    DateTime DataHoraInicio,
    int FechamentoEscalaAntecedenciaEmMinutos
);  