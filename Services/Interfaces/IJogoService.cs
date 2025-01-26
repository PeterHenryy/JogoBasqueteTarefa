using JogoBasqueteTarefa.Models;

namespace JogoBasqueteTarefa.Services.Interfaces
{
    public interface IJogoService
    {
        bool Criar(Jogo jogo);
        Jogo ObterJogoPorID(int jogoID);

        DateTime ObterDataPrimeiroJogo();
        DateTime ObterDataUltimoJogo();
        int ObterQtdJogosDisputados();
        int ObterTotalPontosTemporada();
        int ObterMediaPontosPorJogo();
        int ObterMaiorPontuacaoEmJogo();
        int ObterMenorPontuacaoEmJogo();
        int ObterQtdRecordesBatidos();
    }
}
