using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Organizacoes.Entidades;

namespace Convoquei.Core.Eventos.Servicos.Comandos;

public record CriarEventoComando(
    string Nome,
    string Local,
    string Descricao,
    TipoEventoEnum Tipo,
    DateTime DataHoraInicio,
    TimeSpan FechamentoEscalaAntecedencia,
    MembroOrganizacao Criador,
    Organizacao Organizacao
);