using Convoquei.Api.Responses;
using Convoquei.Application.RecorrenciasEvento.Servicos.Interfaces;
using Convoquei.DataTransfer.Genericos.Responses;
using Convoquei.DataTransfer.RecorrenciasEvento.Requests;
using Convoquei.DataTransfer.RecorrenciasEvento.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convoquei.Api.Controllers.Recorrencias
{
    [ApiController]
    [Authorize]
    [Route("api/organizacoes/{idOrganizacao:guid}/recorrencias-eventos")]
    public class RecorrenciasController : ControllerBase
    {
        private readonly IRecorrenciasEventoAppServico _recorrenciasEventoAppServico;

        public RecorrenciasController(IRecorrenciasEventoAppServico recorrenciasEventoAppServico)
        {
            _recorrenciasEventoAppServico = recorrenciasEventoAppServico;
        }

        /// <summary>
        /// Recuperar uma recorrencia de evento
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="idRecorrencia">ID da recorrência</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idRecorrencia:guid}")]
        public async Task<ActionResult<ApiResponse<RecorrenciaEventoResponse>>> RecuperarAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idRecorrencia, CancellationToken cancellationToken)
        {
            RecorrenciaEventoResponse recorrencia = await _recorrenciasEventoAppServico.RecuperarAsync(idOrganizacao, idRecorrencia, cancellationToken);

            return Ok(ApiResponse<RecorrenciaEventoResponse>.Ok(recorrencia));
        }

        /// <summary>
        /// Listar todas as recorrencias de eventos da organização
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginacaoResponse<RecorrenciaEventoResponse>>>> ListarAsync([FromRoute] Guid idOrganizacao, CancellationToken cancellationToken)
        {
            PaginacaoResponse<RecorrenciaEventoResponse> recorrencias = await _recorrenciasEventoAppServico.ListarAsync(idOrganizacao, 1, 100, cancellationToken);

            return Ok(ApiResponse<PaginacaoResponse<RecorrenciaEventoResponse>>.Ok(recorrencias));
        }

        /// <summary>
        /// Criar uma nova recorrencia de eventos
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="request">Parametros</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<RecorrenciaEventoResponse>>> CriarAsync([FromRoute] Guid idOrganizacao, [FromBody] CriarRecorrenciaRequest request, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Falha(ModelState));

            RecorrenciaEventoResponse recorrencia = await _recorrenciasEventoAppServico.CriarRecorrenciaAsync(idOrganizacao, request, cancellationToken);
            return Ok(ApiResponse<RecorrenciaEventoResponse>.Ok(recorrencia, "Recorrência criada com sucesso!"));
        }

        /// <summary>
        /// Deletar uma recorrencia de evento
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="idRecorrencia">ID da recorrência</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idRecorrencia:guid}")]
        public async Task<IActionResult> DeletarAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idRecorrencia, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Gerar os eventos de uma recorrência de evento
        /// </summary>
        /// <param name="idOrganizacao"></param>
        /// <param name="idRecorrencia"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{idRecorrencia:guid}/geracoes-eventos")]
        public async Task<ActionResult<ApiResponse<int>>> GerarEventosAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idRecorrencia, CancellationToken cancellationToken)
        {
            int eventosGerados = await _recorrenciasEventoAppServico.GerarEventosRecorrenciaAsync(idOrganizacao, idRecorrencia, cancellationToken);

            return Ok(ApiResponse<int>.Ok($"Foram gerados {eventosGerados} eventos para a recorrencia."));
        }
    }
}
