using JogoBasqueteTarefa.Models;

namespace JogoBasqueteTarefa.Repositories.Interfaces
{
    //Esta interface define todos os metodos da aplicação
    public interface IJogoRepository
    {
        Task<bool> Criar(Jogo jogo);
        Task<Jogo> ObterJogoPorID(int jogoID);

        Task<DateTime> ObterDataPrimeiroJogo();
        Task<DateTime> ObterDataUltimoJogo();
        Task<int> ObterQtdJogosDisputados();
        Task<int> ObterTotalPontosTemporada();
        Task<int> ObterMediaPontosPorJogo();
        Task<int> ObterMaiorPontuacaoEmJogo();
        Task<int> ObterMenorPontuacaoEmJogo();
        Task<int> ObterQtdRecordesBatidos();

    }
}
