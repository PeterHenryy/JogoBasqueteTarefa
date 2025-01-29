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

namespace JogoBasqueteTarefa.Test.Repository
{
    public class JogoRepositoryTestes
    {
        private readonly JogoRepository _jogoRepository;
        
        public JogoRepositoryTestes()
        {
            var jogosContext = ObterContext().Result;
            _jogoRepository = new JogoRepository(jogosContext);
        }



        private async Task<ApplicationDbContext> ObterContext()
        {

            var opcoes = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseInMemoryDatabase(databaseName: "BancoDeDadosTeste").Options;
            var jogosContext = new ApplicationDbContext(opcoes);
            jogosContext.Database.EnsureCreated();

            if(!await jogosContext.Jogos.AnyAsync())
            {
                jogosContext.Jogos.Add(new Jogo()
                {
                    Data = DateTime.Now,
                    Pontos = 8
                });
                await jogosContext.SaveChangesAsync();
            }
            return jogosContext;
        }

        [Fact]
        public async Task JogoRepository_Criar_ReturnTrue()
        {
            //Arrange
            Jogo jogo = JogoFaker.CriarJogoFake();

            //Act
            bool resultado = await _jogoRepository.Criar(jogo);

            //Assert
            resultado.Should().BeTrue();

        }
    }
}
