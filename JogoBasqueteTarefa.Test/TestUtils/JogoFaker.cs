using Bogus;
using JogoBasqueteTarefa.Models;

namespace JogoBasqueteTarefa.Test.TestUtils
{

    public static class JogoFaker
    {
        public static Jogo CriarJogoFake()
        {
            var faker = new Faker<Jogo>()
                .RuleFor(j => j.Data, f => f.Date.Past(1)) 
                .RuleFor(j => j.Pontos, f => f.Random.Int(1, 1000)); 

            return faker.Generate();
        }
    }
}
