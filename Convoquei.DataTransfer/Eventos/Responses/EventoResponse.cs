using System.ComponentModel.DataAnnotations;
using Convoquei.Core.Eventos.Entidades;
using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.DataTransfer.Organizacoes.Responses;
using Convoquei.DataTransfer.RecorrenciasEvento.Responses;
using Convoquei.DataTransfer.Usuarios.Response;

namespace Convoquei.DataTransfer.Eventos.Responses;

public record EventoResponse(
  string Nome,
  string Local,
  string Descricao,
  [EnumDataType(typeof(TipoEventoEnum))] int Tipo,
  [EnumDataType(typeof(StatusEventoEnum))]
  int Status,
  DateTime DataHoraInicio,
  TimeSpan FechamentoEscalaAntecedencia,
  UsuarioResponse Criador,
  OrganizacaoResponse Organizacao,
  DadosCancelamentoEventoResponse? Cancelamento,
  IEnumerable<ArquivoEventoResponse> Arquivos,
  IEnumerable<ParticipanteEventoResponse> Participantes,
  RecorrenciaEventoResponse? Recorrencia
)
{
  public static explicit operator EventoResponse(Evento evento)
  {
    return new EventoResponse(
      evento.Nome,
      evento.Local,
      evento.Descricao,
      (int)evento.Tipo,
      (int)evento.Status,
      evento.DataHoraInicio,
      evento.FechamentoEscalaAntecedencia,
      (UsuarioResponse)evento.Criador,
      (OrganizacaoResponse)evento.Organizacao,
      evento.Cancelamento is not null 
        ? (DadosCancelamentoEventoResponse)evento.Cancelamento 
        : null,
      evento.Arquivos.Select(arquivo => (ArquivoEventoResponse)arquivo),
      evento.Participantes.Select(participante => (ParticipanteEventoResponse)participante),
      evento.Recorrencia is not null 
        ? (RecorrenciaEventoResponse)evento.Recorrencia 
        : null
    );
  }
};
