using Convoquei.Api.Responses;
using Convoquei.Application.Organizacoes.Servicos.Interfaces;
using Convoquei.DataTransfer.Genericos.Responses;
using Convoquei.DataTransfer.Organizacoes.Requests;
using Convoquei.DataTransfer.Organizacoes.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convoquei.Api.Controllers.Organizacoes
{
    [Route("api/organizacoes/{idOrganizacao:guid}/convites")]
    [ApiController]
    [Authorize]
    public class OrganizacoesConvitesController : ControllerBase
    {
        private readonly IOrganizacoesConvitesAppServico _organizacoesConvitesAppServico;

        public OrganizacoesConvitesController(IOrganizacoesConvitesAppServico organizacoesConvitesAppServico)
        {
            _organizacoesConvitesAppServico = organizacoesConvitesAppServico;
        }

        /// <summary>
        /// Convidar um usuário para ser membro de uma organização.
        /// </summary>
        /// <param name="idOrganizacao"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ConviteOrganizacaoResponse>>> ConvidarAsync([FromRoute] Guid idOrganizacao, [FromBody] ConvidarMembroRequest request, CancellationToken cancellationToken)
        {
            ConviteOrganizacaoResponse convite = await _organizacoesConvitesAppServico.ConvidarAsync(idOrganizacao, request, cancellationToken);

            return Ok(ApiResponse<ConviteOrganizacaoResponse>.Ok(convite, "Convite enviado com sucesso!"));
        }

        /// <summary>
        /// Listar os convites de uma organização.
        /// </summary>
        /// <param name="idOrganizacao"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginacaoResponse<ConviteOrganizacaoResponse>>>> ListarConvitesAsync([FromRoute] Guid idOrganizacao, [FromQuery] ListarConvitesOrganizacaoRequest request, CancellationToken cancellationToken)
        {
            PaginacaoResponse<ConviteOrganizacaoResponse> convites = await _organizacoesConvitesAppServico.ListarConvitesAsync(idOrganizacao, request, cancellationToken);

            return Ok(ApiResponse<PaginacaoResponse<ConviteOrganizacaoResponse>>.Ok(convites, $"Listando {convites.Total} convites"));
        }

        /// <summary>
        /// Reenviar um convite para ser membro de uma organização.
        /// </summary>
        /// <param name="idOrganizacao"></param>
        /// <param name="idConvite"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{idConvite:guid}/reenvios")]
        public async Task<ActionResult<ApiResponse<ConviteOrganizacaoResponse>>> ReenviarConviteAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idConvite, CancellationToken cancellationToken)
        {
            ConviteOrganizacaoResponse convite = await _organizacoesConvitesAppServico.ReenviarConviteAsync(idOrganizacao, idConvite, cancellationToken);

            return Ok(ApiResponse<ConviteOrganizacaoResponse>.Ok(convite, "Convite reenviado para o usuário."));
        }

        /// <summary>
        /// Aceitar ou recusar um convite para ser membro de uma organização.
        /// </summary>
        /// <param name="idOrganizacao"></param>
        /// <param name="idConvite"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{idConvite:guid}")]
        public async Task<ActionResult<ApiResponse<string>>> ResponderConviteAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idConvite, [FromQuery] ResponderConviteRequest request, CancellationToken cancellationToken)
        {
            var (Sucesso, Mensagem) = await _organizacoesConvitesAppServico.ResponderConviteAsync(idOrganizacao, idConvite, request, cancellationToken);

            if(!Sucesso)
                return BadRequest(ApiResponse<string>.Falha(Mensagem));

            return Ok(ApiResponse<string>.Ok(Mensagem));
        }

        /// <summary>
        /// Deletar um convite para ser membro de uma organização.
        /// </summary>
        /// <param name="idOrganizacao"></param>
        /// <param name="idConvite"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idConvite:guid}")]
        public async Task<IActionResult> RemoverConviteAsync([FromRoute] Guid idOrganizacao, [FromRoute] Guid idConvite, CancellationToken cancellationToken)
        {
            await _organizacoesConvitesAppServico.ExcluirConviteAsync(idOrganizacao, idConvite, cancellationToken);

            return Ok(ApiResponse<string>.Ok("Convite excluido com sucesso!"));
        }
    }
}
