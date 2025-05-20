using Convoquei.Application.Usuarios.Excecoes;
using Convoquei.Application.Usuarios.Servicos.Interfaces;
using Convoquei.DataTransfer.Usuarios.Request;
using Convoquei.DataTransfer.Usuarios.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convoquei.Api.Controllers.Usuarios
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IAutenticacaoAppServico _usuariosAppServico;

        public UsuariosController(IAutenticacaoAppServico usuariosAppServico)
        {
            _usuariosAppServico = usuariosAppServico;
        }

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AutenticadoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LogarRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                AutenticadoResponse response = await _usuariosAppServico.AutenticarAsync(request, cancellationToken);
                return Ok(response);
            }
            catch(CredenciaisInvalidasExcecao)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status201Created)]
        [Route("cadastro")]
        public async Task<ActionResult> Cadastrar([FromBody] CadastrarRequest dados, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UsuarioResponse usuario = await _usuariosAppServico.CadastrarAsync(dados, cancellationToken);

            return CreatedAtAction(nameof(Me), new { id = usuario.Id }, usuario);
        }

        /// <summary>
        /// Atualiza o token de acesso do usuário.
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshRequest dados, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Logout do usuário.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("logout")]
        public async Task<ActionResult> Logout(CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Retorna os dados do usuário logado.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("me")]
        public async Task<ActionResult<UsuarioResponse>> Me(CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
