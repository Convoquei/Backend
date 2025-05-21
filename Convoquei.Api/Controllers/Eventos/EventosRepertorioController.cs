using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convoquei.Api.Controllers.Eventos
{
    [Route("api/organizacoes/{idOrganizacao:guid}/eventos/{idEvento:guid}/repertorios")]
    [ApiController]
    [Authorize]
    public class EventosRepertorioController : ControllerBase
    {
        /// <summary>
        /// Adicionar um repertório a um evento
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="idEvento">ID do evento</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InserirAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idEvento, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Remover um repertório de um evento
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="idEvento">ID do evento</param>
        /// <param name="idRepertorio">ID do repertório</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idRepertorio:guid}")]
        public async Task<IActionResult> RemoverAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idEvento, [FromRoute] Guid idRepertorio, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Recuperar um repertório de um evento
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="idEvento">ID do evento</param>
        /// <param name="idRepertorio"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idRepertorio:guid}")]
        public async Task<IActionResult> RecuperarAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idEvento, [FromRoute] Guid idRepertorio, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Listar repertórios de um evento
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="idEvento">ID do evento</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListarAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idEvento, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Alterar a posição de um repertório em um evento
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="idEvento">ID do evento</param>
        /// <param name="idRepertorio">ID do repertório</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{idRepertorio:guid}")]
        public async Task<IActionResult> AlterarPosicaoAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idEvento, [FromRoute] Guid idRepertorio, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
