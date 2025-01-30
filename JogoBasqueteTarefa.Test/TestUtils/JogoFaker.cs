using Bogus;
using JogoBasqueteTarefa.Models;

namespace JogoBasqueteTarefa.Test.TestUtils
{

    public static class JogoFaker
    {
        public static Jogo CriarJogoFake()
        {
            //definindo regras para o jogo a ser criado
            var faker = new Faker<Jogo>()
                .RuleFor(j => j.Data, f => f.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now.AddYears(-1)))
                .RuleFor(j => j.Pontos, f => f.Random.Int(1, 500)); 

            return faker.Generate();
        }

        //método para gerar determinada quantidade de jogos falsos
        public static List<Jogo> CriarJogosFakes(int quantidade)
        {
            return Enumerable.Range(0, quantidade)
                             .Select(_ => CriarJogoFake())
                             .ToList();
        }
    }
}
