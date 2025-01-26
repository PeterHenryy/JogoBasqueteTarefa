namespace JogoBasqueteTarefa.Models
{
    public class Resultados
    {
        public DateTime? DataPrimeiroJogo { get; set; }
        public DateTime DataUltimoJogo { get; set; }
        public int JogosDisputados { get; set; }
        public int TotalPontosTemporada { get; set; }
        public int MediaPontosPorJogo { get; set; }
        public int MaiorPontuacaoEmJogo { get; set; }
        public int MenorPontuacaoEmJogo { get; set; }
        public int QtdVezesBateuRecorde { get; set; }
    }
}
