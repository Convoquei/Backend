using System.ComponentModel.DataAnnotations;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.DataTransfer.Organizacoes.Responses;
using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.DataTransfer.Eventos.Responses;

public record EventoResponse(
  string Nome,
  string Local,
  string Descricao,
  [EnumDataType(typeof(TipoEventoEnum))]
  int Tipo,
  [EnumDataType(typeof(StatusEventoEnum))]
  int Status,
  DateTime DataHoraInicio,
  TimeSpan FechamentoEscalaAntecedencia,
  UsuarioResponse Criador,
  OrganizacaoResponse Organizacao,
  DadosCancelamentoEventoResponse? Cancelamento,
  IEnumerable<ArquivoEventoResponse> Arquivos,
  IEnumerable<ArquivoEventoResponse> Participantes,
  RecorrenciaEventoBase? Recorrencia
);

public record DadosCancelamentoEventoResponse(
  DateTime Data,
  string Motivo,
  UsuarioResponse Usuario
);

public record ArquivoEventoResponse(
 EventoResponse Evento,
 string Nome,
 string MimeType,
 long TamanhoEmBytes,
 string ChaveStorage,
 UsuarioResponse Criador
);