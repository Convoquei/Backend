using System.ComponentModel.DataAnnotations;
using Convoquei.Core.Eventos.Enumeradores;

namespace Convoquei.DataTransfer.Eventos.Requests;

public record CriarEventoRequest(
    string Nome,
    string Local,
    string Descricao,
    [EnumDataType(typeof(TipoEventoEnum))] 
    int Tipo,
    [EnumDataType(typeof(StatusEventoEnum))] 
    int Status,
    DateTime DataHoraInicio,
    TimeSpan FechamentoEscalaAntecedencia
);  