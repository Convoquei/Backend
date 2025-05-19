using Convoquei.Application.Organizacoes.Servicos.Interfaces;
using Convoquei.DataTransfer.Organizacoes.Requests;
using Convoquei.DataTransfer.Organizacoes.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Convoquei.Api.Controllers.Organizacoes
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizacoesController : ControllerBase
    {
        private readonly IOrganizacoesAppServico _organizacoesAppServico;

        public OrganizacoesController(IOrganizacoesAppServico organizacoesAppServico) 
        {
            _organizacoesAppServico = organizacoesAppServico;
        }

        /// <summary>
        /// Criar organização
        /// </summary>
        /// <param name="request">Dados da organizacao</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<OrganizacaoResponse>> CriarOrganizacao([FromBody] CriarOrganizacaoRequest request, CancellationToken cancellationToken)
        {
            OrganizacaoResponse response = await _organizacoesAppServico.CriarAsync(request, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Listar organizações
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListarOrganizacoes(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
