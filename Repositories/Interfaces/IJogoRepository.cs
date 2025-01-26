using JogoBasqueteTarefa.Models;

namespace JogoBasqueteTarefa.Repositories.Interfaces
{
    public interface IJogoRepository
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
