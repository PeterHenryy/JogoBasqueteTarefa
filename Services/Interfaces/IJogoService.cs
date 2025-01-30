using JogoBasqueteTarefa.Models;

namespace JogoBasqueteTarefa.Services.Interfaces
{
    //interface service com os mesmos métodos que a interface repository
    public interface IJogoService
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
        Task<Resultados> ObterResultadosDosJogos();
    }
}
