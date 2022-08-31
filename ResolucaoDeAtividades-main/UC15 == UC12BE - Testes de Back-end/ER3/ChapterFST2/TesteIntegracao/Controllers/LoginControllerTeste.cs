using ChapterFST2.Controllers;
using ChapterFST2.Interfaces;
using ChapterFST2.Models;
using ChapterFST2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using Xunit;

namespace TesteIntegracao.Controllers
{
    public class LoginControllerTeste
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {
            //Arrange - Prepara��o
            var repositorioEspelhado = new Mock<IUsuarioRepository>();

            repositorioEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            LoginViewModel dados = new LoginViewModel();
            dados.Email = "email@email.com";
            dados.Senha = "123456";

            var controller = new LoginController(repositorioEspelhado.Object);

            //Act - A��o
            var resultado = controller.Login(dados);

            //Assert - Verifica��o
            Assert.IsType<UnauthorizedObjectResult>(resultado);
        }



        [Fact]
        public void LoginController_Retornar_Token()
        {
            //Arrange - Prepara��o
            Usuario usuarioRetorno = new Usuario();
            usuarioRetorno.Email = "email@email.com";
            usuarioRetorno.Senha = "1234";
            usuarioRetorno.Tipo = "1";
            usuarioRetorno.Id = 1;

            var repositorioEspelhado = new Mock<IUsuarioRepository>();

            repositorioEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetorno);

            LoginViewModel dados = new LoginViewModel();
            dados.Email = "batata";
            dados.Senha = "1234";

            var controller = new LoginController(repositorioEspelhado.Object);

            string issuerValido = "chapter.webapi";

            //Act - A��o
            OkObjectResult resultado = (OkObjectResult)controller.Login(dados);

            string tokenString = resultado.Value.ToString().Split(' ')[3];

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenJwt = jwtHandler.ReadJwtToken(tokenString);

            //Assert - Verifica��o
            Assert.Equal(issuerValido, tokenJwt.Issuer);
        }
    }
}