using Convoquei.Api.Responses;
using Convoquei.Application.RecorrenciasEvento.Servicos.Interfaces;
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
        public async Task<IActionResult> RecuperarAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idRecorrencia, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Listar todas as recorrencias de eventos da organização
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListarAsync([FromRoute] Guid idOrganizacao, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Criar uma nova recorrencia de eventos
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="request"></param>
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
    }
}
