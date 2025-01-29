using FluentAssertions;
using JogoBasqueteTarefa.Controllers;
using JogoBasqueteTarefa.Models;
using JogoBasqueteTarefa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FakeItEasy;
using JogoBasqueteTarefa.Test.TestUtils;

namespace JogoBasqueteTarefa.Test.Controller
{
    public class JogoControllerTeste
    {
        private readonly IJogoService _jogoService;
        private readonly JogoController _jogoController;


        public JogoControllerTeste()
        {
            _jogoService = A.Fake<IJogoService>();
            _jogoController = new JogoController(_jogoService);

        }

        [Fact]
        public async Task JogoController_Criar_ReturnCreated()
        {
            //Arrange
            Jogo jogo = JogoFaker.CriarJogoFake();

            //Act
            A.CallTo(() => _jogoService.Criar(jogo)).Returns(true);
            var resultado = (CreatedAtActionResult)await _jogoController.Criar(jogo);

            //Assert
            resultado.StatusCode.Should().Be(201);
            resultado.Should().NotBeNull();

        }

        [Fact]
        public async Task JogoController_Criar_ReturnServerError()
        {
            // Arrange
            Jogo jogo = JogoFaker.CriarJogoFake();

            A.CallTo(() => _jogoService.Criar(jogo)).Throws(new Exception("Erro banco de dados"));

            // Act
            var resultado = (ObjectResult)await _jogoController.Criar(jogo);

            // Assert
            resultado.StatusCode.Should().Be(500);
            resultado.Should().NotBeNull();
            resultado.Value.Should().Be("Não foi possível criar o jogo: Erro banco de dados");
        }

        [Fact]
        public async Task JogoController_Criar_DataInvalida_ReturnBadRequest()
        {

            //Arrange
            Jogo jogo = JogoFaker.CriarJogoFake();
            jogo.Data = DateTime.Now.AddDays(5);

            // Act
            var resultado = (BadRequestObjectResult)await _jogoController.Criar(jogo);

            // Assert
            resultado.StatusCode.Should().Be(400);
            resultado.Should().NotBeNull();
            resultado.Value.Should().BeEquivalentTo(new { message = "Algo deu errado ao tentar criar jogo" });
        }
        [Fact]
        public async Task JogoController_Criar_PontosMenorQueZero_ReturnBadRequest()
        {
            //Arrange
            Jogo jogo = JogoFaker.CriarJogoFake();
            jogo.Pontos = -1;

            // Act
            var resultado = (BadRequestObjectResult)await _jogoController.Criar(jogo);

            // Assert
            resultado.StatusCode.Should().Be(400);
            resultado.Should().NotBeNull();
            resultado.Value.Should().BeEquivalentTo(new { message = "Algo deu errado ao tentar criar jogo" });

        }

        [Fact]
        public async Task JogoController_ObterResultadosJogos_JogosDisputadosMaiorQueZero_ReturnsOk()
        {
            // Arrange
            var fakeResultados = ResultadosFaker.CriarResultadosFake();

            A.CallTo(() => _jogoService.ObterQtdJogosDisputados()).Returns(5);
            A.CallTo(() => _jogoService.ObterResultadosDosJogos()).Returns(Task.FromResult(fakeResultados));

            // Act
            var resultado = await _jogoController.ObterResultadosJogos();

            // Assert
            resultado.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<Resultados>()
                .And.BeSameAs(fakeResultados);
        }

        [Fact]
        public async Task JogoController_ObterResultadosJogos_JogosDisputadosForZero_ReturnsBadRequest()
        {
            // Arrange
            A.CallTo(() => _jogoService.ObterQtdJogosDisputados()).Returns(0);

            // Act
            var resultado = await _jogoController.ObterResultadosJogos();

            // Assert
            resultado.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { message = "Não há nenhum jogo disponível" });
        }

    }
}