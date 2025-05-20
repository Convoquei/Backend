using Convoquei.Core.Seguranca.Servicos;
using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Convoquei.Infra.Seguranca.Servicos
{
    public class TokenServico : ITokenServico
    {
        private readonly IConfiguration _configuration;

        public TokenServico(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token GerarToken(Usuario usuario)
        {
            string token = GerarToken(usuario, 15);
            string refreshToken = GerarToken(usuario, 60 * 24 * 7);

            return new Token(token, refreshToken);
        }

        public string GerarToken(Usuario usuario, int minutosExpiracao)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email.Endereco.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(minutosExpiracao),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
