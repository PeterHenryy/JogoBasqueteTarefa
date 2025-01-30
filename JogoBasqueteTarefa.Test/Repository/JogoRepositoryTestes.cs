using JogoBasqueteTarefa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using JogoBasqueteTarefa.Models;
using JogoBasqueteTarefa.Repositories;
using FluentAssertions;
using JogoBasqueteTarefa.Test.TestUtils;
using Bogus;

namespace JogoBasqueteTarefa.Test.Repository
{
    public class JogoRepositoryTestes
    {
        //criando novo banco de dados para testes
        private async Task<ApplicationDbContext> ObterContext(string nomeBanco = "BancoDeDadosTeste")
        {
            var opcoes = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseInMemoryDatabase(databaseName: nomeBanco).Options;
            var jogosContext = new ApplicationDbContext(opcoes);
            jogosContext.Database.EnsureCreated();
            return jogosContext;
        }

        //método para limpar banco de dados com o fim de que nenhum dado residual de um teste afete outro teste
        //método a ser chamado em todos os testes 
        private async Task LimparBanco(ApplicationDbContext context)
        {
            context.Jogos.RemoveRange(context.Jogos);
            await context.SaveChangesAsync();
        }

        
        [Fact]
        public async Task JogoRepository_Criar_ReturnTrue()
        {
            //Arrange
            Jogo jogo = JogoFaker.CriarJogoFake();
            var context = await ObterContext();
            await LimparBanco(context);
            JogoRepository jogoRepository = new JogoRepository(context);

            //Act
            bool resultado = await jogoRepository.Criar(jogo);

            //Assert
            
            resultado.Should().BeTrue();
            var createdJogo = context.Jogos.FindAsync(jogo.ID);
            createdJogo.Should().NotBeNull();

        }

        [Fact]
        public async Task JogoRepository_Criar_DataInvalida_ReturnFalse()
        {
            //Arrange
            Jogo jogo = JogoFaker.CriarJogoFake();
            jogo.Data = DateTime.Now.AddDays(10);
            var context = await ObterContext();
            await LimparBanco(context);
            JogoRepository jogoRepository = new JogoRepository(context);

            //Act
            bool resultado = await jogoRepository.Criar(jogo);

            //Assert
            resultado.Should().BeFalse();
            var count = await context.Jogos.CountAsync();
            count.Should().Be(0);

        }

        [Fact]
        public async Task JogoRepository_Criar_PontosMenoresQueUm_ReturnFalse()
        {
            //Arrange
            Jogo jogo = JogoFaker.CriarJogoFake();
            jogo.Pontos = 0;
            var context = await ObterContext();
            await LimparBanco(context);
            JogoRepository jogoRepository = new JogoRepository(context);

            //Act
            bool resultado = await jogoRepository.Criar(jogo);

            //Assert
            resultado.Should().BeFalse();
            var count = await context.Jogos.CountAsync();
            count.Should().Be(0);
        }



        [Fact]
        public async Task JogoRepository_ObterJogoPorID_ReturnJogo()
        {
            // Arrange
            var context = await ObterContext();
            await LimparBanco(context); 
            JogoRepository jogoRepository = new JogoRepository(context);

            var jogoExistente = JogoFaker.CriarJogoFake();
            context.Jogos.Add(jogoExistente);
            await context.SaveChangesAsync();

            // Act
            var resultado = await jogoRepository.ObterJogoPorID(jogoExistente.ID);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.ID.Should().Be(jogoExistente.ID);
            resultado.Pontos.Should().Be(jogoExistente.Pontos);
        }

        [Fact]
        public async Task JogoRepository_ObterJogoPorID_IdInexistente_ReturnNull()
        {
            // Arrange
            var context = await ObterContext();
            await LimparBanco(context);
            JogoRepository jogoRepository = new JogoRepository(context);
            var idInexistente = -1;

            // Act
            var resultado = await jogoRepository.ObterJogoPorID(idInexistente);

            // Assert
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task JogoRepository_ObterDataPrimeiroJogo_ReturnMenorData()
        {
            // Arrange
            var context = await ObterContext();
            await LimparBanco(context); 
            JogoRepository repository = new JogoRepository(context);

            var jogos = JogoFaker.CriarJogosFakes(5);
            jogos.Add(new Jogo { Data = new DateTime(2020, 1, 1), Pontos = 10 });

            context.Jogos.AddRange(jogos);
            await context.SaveChangesAsync();

            // Act
            var resultado = await repository.ObterDataPrimeiroJogo();

            // Assert
            resultado.Should().Be(new DateTime(2020, 1, 1));
        }

        [Fact]
        public async Task JogoRepository_ObterDataUltimoJogo_ReturnMaiorData()
        {
            // Arrange
            var context = await ObterContext();
            await LimparBanco(context); 
            JogoRepository repository = new JogoRepository(context);

            var jogos = JogoFaker.CriarJogosFakes(5);

            var jogoFuturo = new Jogo { Data = new DateTime(DateTime.Now.Year, 01, 01), Pontos = 10 };
            jogos.Add(jogoFuturo);

            context.Jogos.AddRange(jogos);
            await context.SaveChangesAsync();

            // Act
            var resultado = await repository.ObterDataUltimoJogo();

            // Assert
            resultado.Should().Be(jogoFuturo.Data); 
        }

        [Fact]
        public async Task JogoRepository_ObterQtdJogosDisputados_ReturnContagem()
        {
            // Arrange
            var context = await ObterContext();
            await LimparBanco(context); 
            JogoRepository repository = new JogoRepository(context);

            var jogos = JogoFaker.CriarJogosFakes(10);
            context.Jogos.AddRange(jogos);
            await context.SaveChangesAsync();

            // Act
            var resultado = await repository.ObterQtdJogosDisputados();

            // Assert
            resultado.Should().Be(10);
        }

        [Fact]
        public async Task JogoRepository_ObterMaiorPontuacaoEmJogo_ReturnPontuacaoMaxima()
        {
            // Arrange
            var context = await ObterContext();
            await LimparBanco(context); 
            JogoRepository repository = new JogoRepository(context);

            var jogos = JogoFaker.CriarJogosFakes(10); ;
            jogos.Add(new Jogo { Data = DateTime.Now, Pontos = 505 });

            context.Jogos.AddRange(jogos);
            await context.SaveChangesAsync();

            // Act
            var resultado = await repository.ObterMaiorPontuacaoEmJogo();

            // Assert
            resultado.Should().Be(505);
        }

        [Fact]
        public async Task JogoRepository_ObterMenorPontuacaoEmJogo_ReturnPontuacaoMinima()
        {
            // Arrange
            var context = await ObterContext();
            await LimparBanco(context);
            JogoRepository repository = new JogoRepository(context);

            var jogos = JogoFaker.CriarJogosFakes(10); ;
            jogos.Add(new Jogo { Data = DateTime.Now, Pontos = 1});

            context.Jogos.AddRange(jogos);
            await context.SaveChangesAsync();

            // Act
            var resultado = await repository.ObterMenorPontuacaoEmJogo();

            // Assert
            resultado.Should().Be(1);
        }

        [Fact]
        public async Task JogoRepository_ObterTotalPontosTemporada_ReturnSomaDePontos()
        {
            // Arrange
            var context = await ObterContext();
            await LimparBanco(context); 


            var repository = new JogoRepository(context);

            var jogos = JogoFaker.CriarJogosFakes(5); 
            foreach (var jogo in jogos)
            {
                context.Jogos.Add(jogo);
            }
            await context.SaveChangesAsync();

            var totalPontos = jogos.Sum(x => x.Pontos);

            // Act
            var resultado = await repository.ObterTotalPontosTemporada();

            // Assert
            resultado.Should().Be(totalPontos); 
        }

        [Fact]
        public async Task JogoRepository_ObterMediaPontosPorJogo_ReturnMediaPontos()
        {
            // Arrange
            var context = await ObterContext();
            await LimparBanco(context);

            JogoRepository repository = new JogoRepository(context);

            var jogos = JogoFaker.CriarJogosFakes(5);
            foreach (var jogo in jogos)
            {
                context.Jogos.Add(jogo);
            }
            await context.SaveChangesAsync();

            var mediaPontos = (int)jogos.Average(x => x.Pontos);

            // Act
            var resultado = await repository.ObterMediaPontosPorJogo();

            // Assert
            resultado.Should().Be(mediaPontos);

        }

        [Fact]
        public async Task JogoRepository_ObterQtdRecordesBatidos_ReturnContagemDeRecordes()
        {
            // Arrange

            //Este teste não funciona de jeito nenhum sem criar um novo banco
            var context = await ObterContext("NovoBanco");

            JogoRepository repository = new JogoRepository(context);

            var jogos = new List<Jogo>
            {
                new() { Data = DateTime.Now.AddDays(-5), Pontos = 2 },
                new() { Data = DateTime.Now.AddDays(-7), Pontos = 5 },
                new() { Data = DateTime.Now.AddDays(-8), Pontos = 1 },
                new() { Data = DateTime.Now.AddDays(-1), Pontos = 10 }
            };

            context.Jogos.AddRange(jogos);
            await context.SaveChangesAsync();

            // Act
            var resultado = await repository.ObterQtdRecordesBatidos();

            // Assert
            resultado.Should().Be(2);
        }
    }
}
