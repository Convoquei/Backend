using Convoquei.Core.Genericos.ValueObjects;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Tests.Usuarios.Entidades
{
    public class UsuarioTestes
    {
        private readonly Usuario sut;

        public UsuarioTestes()
        {
            sut = new Usuario("Gankina Anastasiya Kononovna", (Email)"gankinaanastasya@hotmail.com");
        }

        public class Construtor : UsuarioTestes
        {
            [Fact]
            public void Quando_CriarUsuario_Espero_InicializarComNomeEEmailCorreto()
            {
                // Arrange
                string nomeEsperado = "Jeovana";
                string emailEsperado = "geovana@gmail.com";

                // Act
                var usuario = new Usuario(nomeEsperado, (Email)emailEsperado);

                // Assert
                Assert.Equal(nomeEsperado, usuario.Nome);
                Assert.Equal(emailEsperado, usuario.Email.Valor);
            }
        }

        public class AdicionarOrganizacaoMetodo : UsuarioTestes
        {
            [Fact]
            public void Quando_AdicionarOrganizacao_Espero_OrganizacaoAdicionada()
            {

            }
        }
    }
}
