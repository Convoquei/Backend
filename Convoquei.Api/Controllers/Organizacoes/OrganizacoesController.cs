using Convoquei.Api.Responses;
using Convoquei.Application.Organizacoes.Servicos.Interfaces;
using Convoquei.DataTransfer.Genericos.Responses;
using Convoquei.DataTransfer.Organizacoes.Requests;
using Convoquei.DataTransfer.Organizacoes.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convoquei.Api.Controllers.Organizacoes
{
    [Route("api/organizacoes")]
    [ApiController]
    [Authorize]
    public class OrganizacoesController : ControllerBase
    {
        private readonly IOrganizacoesAppServico _organizacoesAppServico;

        public OrganizacoesController(IOrganizacoesAppServico organizacoesAppServico) 
        {
            _organizacoesAppServico = organizacoesAppServico;
        }

        /// <summary>
        /// Criar uma nova organização
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<OrganizacaoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<OrganizacaoResponse>>> CriarOrganizacao([FromBody] CriarOrganizacaoRequest request, CancellationToken cancellationToken)
        {
            OrganizacaoResponse response = await _organizacoesAppServico.CriarAsync(request, cancellationToken);

            return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, ApiResponse<OrganizacaoResponse>.Ok(response, "Organização criada com sucesso."));
        }

        /// <summary>
        /// Listar organizações
        /// </summary>
        /// <param name="request">Filtros</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginacaoResponse<OrganizacaoResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<PaginacaoResponse<OrganizacaoResponse>>>> ListarOrganizacoes([FromQuery] ListarOrganizacoesRequest request, CancellationToken cancellationToken)
        {
            PaginacaoResponse<OrganizacaoResponse> response = await _organizacoesAppServico.ListarAsync(request, cancellationToken);

            return Ok(ApiResponse<PaginacaoResponse<OrganizacaoResponse>>.Ok(response, $"Listando {response.Total} organizações."));
        }

        /// <summary>
        /// Recuperar uma organização
        /// </summary>
        /// <param name="id">ID da organização</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<OrganizacaoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<OrganizacaoResponse>>> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            OrganizacaoResponse? response = await _organizacoesAppServico.RecuperarAsync(id, cancellationToken);
            if(response is null)
                return BadRequest(ApiResponse<OrganizacaoResponse>.Falha("Organização não encontrada."));

            return Ok(ApiResponse<OrganizacaoResponse>.Ok(response, "Organização encontrada"));
        }
    }
}
