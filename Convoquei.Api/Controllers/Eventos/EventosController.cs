using Convoquei.Application.Eventos.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convoquei.Api.Controllers.Eventos
{
    [Route("api/organizacoes/{idOrganizacao:guid}/eventos")]
    [ApiController]
    [Authorize]
    public class EventosController : ControllerBase
    {
        private readonly IEventosAppServico eventosAppServico;

        public EventosController(IEventosAppServico eventosAppServico)
        {
            this.eventosAppServico = eventosAppServico;
        }

        /// <summary>
        /// Criar um novo evento
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CriarEventoAsync([FromRoute] Guid idOrganizacao, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Recuperar um evento específico
        /// </summary>
        /// <param name="id">ID do evento</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idEvento:guid}")]
        public async Task<IActionResult> RecuperarAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idEvento, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Lista todos os eventos da organização
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListarAsync([FromRoute] Guid idOrganizacao, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Informar disponibilidade para um evento
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{idEvento:guid}/disponibilidades")]
        public async Task<IActionResult> InformarDisponibilidadeAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idEvento, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Cancelar um evento
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{idEvento:guid}/cancelamentos")]
        public async Task<IActionResult> CancelarEventoAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idEvento, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Alterar o status de uma escala
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{idEvento:guid}/escala/{idUsuario:guid}/status")]
        public async Task<IActionResult> ModificarEscalaAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idEvento, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
