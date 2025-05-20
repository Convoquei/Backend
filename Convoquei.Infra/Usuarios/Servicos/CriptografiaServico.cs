using Convoquei.Core.Usuarios.Servicos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Convoquei.Infra.Usuarios.Servicos
{
    public class CriptografiaServico : ICriptografiaServico
    {
        private static readonly PasswordHasher<object> _hasher = CriarHasher();

        private static PasswordHasher<object> CriarHasher()
        {
            var options = Options.Create(new PasswordHasherOptions
            {
                CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3,
                IterationCount = 15000
            });

            return new PasswordHasher<object>(options);
        }

        public string Criptografar(string senha)
        {
            return _hasher.HashPassword(null!, senha);
        }

        public bool Validar(string senhaHash, string senhaSemHash)
        {
            return _hasher.VerifyHashedPassword(null!, senhaHash, senhaSemHash) == PasswordVerificationResult.Success; ;
        }
    }
}