using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convoquei.Api.Controllers.Organizacoes
{
    [Route("api/organizacoes/{idOrganizacao:guid}/membros")]
    [ApiController]
    [Authorize]
    public class OrganizacoesMembrosController : ControllerBase
    {
        /// <summary>
        /// Remover um membro da organização
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="idMembro">ID do membro</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idMembro:guid}")]
        public async Task<ActionResult> RemoverMembroAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idMembro, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Alterar o cargo de um membro da organização
        /// </summary>
        /// <param name="idOrganizacao">ID da organização</param>
        /// <param name="idMembro">ID do membro</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{idMembro:guid}")]
        public async Task<ActionResult> AlterarCargoMembroAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idMembro, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
