using FakeItEasy;
using FluentAssertions;
using JogoBasqueteTarefa.Controllers;
using JogoBasqueteTarefa.Models;
using JogoBasqueteTarefa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace JogoBasqueteTarefa.Test.Controller
{
    public class JogoControllerTeste
    {
        private readonly IJogoService _jogoService;
        private readonly JogoController _jogoController;

        public JogoControllerTeste()
        {
            this._jogoService = A.Fake<IJogoService>();
            this._jogoController = new JogoController(_jogoService);

        }

        private static Jogo CriarJogoFake() => A.Fake<Jogo>();

        [Fact]
        public async Task JogoController_Criar_ReturnCreated()
        {
            //Arrange
            Jogo jogo = CriarJogoFake();

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
            Jogo jogo = CriarJogoFake(); 

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
            
            Jogo jogo = CriarJogoFake();
            jogo.Data = DateTime.Now.AddDays(5);  

            // Act
            var resultado = (BadRequestObjectResult)await _jogoController.Criar(jogo); 

            // Assert
            resultado.StatusCode.Should().Be(400); 
            resultado.Should().NotBeNull();
            resultado.Value.Should().BeEquivalentTo(new { message = "A data do jogo não pode ser depois da data atual." }); 
        }

        [Fact]
        public async Task JogoController_ObterResultadosJogos_JogosDisputadosMaiorQueZero_ReturnsOk_()
        {
            // Arrange
            var fakeResultados = A.Fake<Resultados>();

            A.CallTo(() => _jogoService.ObterQtdJogosDisputados()).Returns(5);
            A.CallTo(() => _jogoService.ObterResultadosDosJogos()).Returns(Task.FromResult(fakeResultados));

            var controller = new JogoController(_jogoService);

            // Act
            var resultado = await controller.ObterResultadosJogos();

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

            var controller = new JogoController(_jogoService);

            // Act
            var result = await controller.ObterResultadosJogos();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { message = "Não há nenhum jogo disponível" });
        }
    }
}