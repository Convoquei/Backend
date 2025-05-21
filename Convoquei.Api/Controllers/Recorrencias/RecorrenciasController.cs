using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convoquei.Api.Controllers.Recorrencias
{
    [ApiController]
    [Authorize]
    [Route("api/organizacoes/{idOrganizacao:guid}/recorrencias-eventos")]
    public class RecorrenciasController : ControllerBase
    {
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
        /// <param name="idOrganizacao"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CriarAsync([FromRoute] Guid idOrganizacao, CancellationToken cancellationToken)
        {
            return Ok();
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
