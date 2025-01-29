using Bogus;
using JogoBasqueteTarefa.Models;


namespace JogoBasqueteTarefa.Test.TestUtils
{
    public static class ResultadosFaker
    {
        public static Resultados CriarResultadosFake()
        {
            var faker = new Faker<Resultados>()
            .RuleFor(r => r.DataPrimeiroJogo, f => f.Date.Past(1)) 
            .RuleFor(r => r.DataUltimoJogo, f => f.Date.Recent()) 
            .RuleFor(r => r.JogosDisputados, f => f.Random.Int(1, 1000)) 
            .RuleFor(r => r.TotalPontosTemporada, f => f.Random.Int(0, 50000)) 
            .RuleFor(r => r.MediaPontosPorJogo, f => f.Random.Int(0, 500)) 
            .RuleFor(r => r.MaiorPontuacaoEmJogo, f => f.Random.Int(0, 500)) 
            .RuleFor(r => r.MenorPontuacaoEmJogo, f => f.Random.Int(0, 500)) 
            .RuleFor(r => r.QtdVezesBateuRecorde, f => f.Random.Int(0, 510));

            return faker.Generate();
        }
    }
}
