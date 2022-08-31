using ProjetoTesteFST2;
using Xunit;

namespace TestXUnit
{
    public class TesteCalculadoraXUnit
    {
        [Fact]
        public void TesteSomarDoisNumeros()
        {
            //Arrange - Prepara��o
            double pNum = 1;
            double sNum = 1;
            double tNum = 2;

            //Act - A��o
            var resultado = Calculadora.Somar(pNum, sNum);

            //Assert - Verifica��o
            Assert.Equal(tNum, resultado);
        }


        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        [InlineData(1, 2, 2)]
        public void TesteSomarDoisNumerosLista(double pNum, double sNum, double tNum)
        {
            //Act - A��o
            var resultado = Calculadora.Somar(pNum, sNum);

            //Assert - Verifica��o
            Assert.Equal(tNum, resultado);
        }
    }
}