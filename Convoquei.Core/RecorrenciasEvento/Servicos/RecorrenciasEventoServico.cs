using Convoquei.Core.Eventos.Enumeradores;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Entidades;
using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.RecorrenciasEvento.Entidades;
using Convoquei.Core.RecorrenciasEvento.Servicos.Comandos;
using Convoquei.Core.RecorrenciasEvento.Servicos.Interfaces;

namespace Convoquei.Core.RecorrenciasEvento.Servicos
{
    public class RecorrenciasEventoServico : IRecorrenciasEventoServico
    {
        public RecorrenciaEventoBase CriarRecorrencia(CriarRecorrenciaEventoComando comando)
        {
            Organizacao organizacao = comando.Organizacao;
            MembroOrganizacao membro = organizacao.ValidarMembro(comando.Criador);

            RecorrenciaEventoBase recorrencia = GerarRecorrencia(comando);

            organizacao.AdicionarRecorrencia(membro, recorrencia);

            return recorrencia;
        }

        private static RecorrenciaEventoBase GerarRecorrencia(CriarRecorrenciaEventoComando comando)
        {
            return comando.Tipo switch
            {
                TipoEventoEnum.IntervaloDias => GerarRecorrenciaIntervaloDias(comando),
                TipoEventoEnum.Semanal => GerarRecorrenciaSemanal(comando),
                _ => throw new RegraDeNegocioExcecao("Não é possível criar uma recorrencia para esse tipo de evento.")
            };
        }

        private static RecorrenciaEventoPeriodico GerarRecorrenciaIntervaloDias(CriarRecorrenciaEventoComando comando)
        {
            return new RecorrenciaEventoPeriodico(
                comando.Nome,
                comando.Local,
                comando.Descricao,
                comando.DataHoraInicio,
                comando.FechamentoEscalaAntecedencia,
                comando.Criador,
                comando.Organizacao,
                comando.IntervaloDias ?? throw new RegraDeNegocioExcecao("Para criar evento com recorrencia diária, é necessário informar o intervalo de dias."));
        }

        private static RecorrenciaEventoSemanal GerarRecorrenciaSemanal(CriarRecorrenciaEventoComando comando)
        {
            return new RecorrenciaEventoSemanal(
                comando.Nome,
                comando.Local,
                comando.Descricao,
                comando.DataHoraInicio,
                comando.FechamentoEscalaAntecedencia,
                comando.Criador,
                comando.Organizacao,
                comando.DiasSemanaBitmap ?? throw new RegraDeNegocioExcecao("Para criar evento com recorrencia semanal, é necessário informar os dias da semana."));
        }
    }
}
