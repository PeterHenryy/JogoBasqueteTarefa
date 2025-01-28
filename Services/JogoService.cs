using JogoBasqueteTarefa.Models;
using JogoBasqueteTarefa.Repositories;
using JogoBasqueteTarefa.Services.Interfaces;

namespace JogoBasqueteTarefa.Services
{
    public class JogoService : IJogoService
    {
        private readonly JogoRepository _jogoRepository;

        public JogoService(JogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<bool> Criar(Jogo jogo)
        {
            bool jogoCriado = await _jogoRepository.Criar(jogo);
            return jogoCriado;
        }

        public async Task<Jogo> ObterJogoPorID(int jogoID)
        {
            Jogo jogo = await _jogoRepository.ObterJogoPorID(jogoID);
            return jogo;
        }

        public async Task<DateTime> ObterDataPrimeiroJogo()
        {
            DateTime data = await _jogoRepository.ObterDataPrimeiroJogo();
            return data;
        }

        public async Task<DateTime> ObterDataUltimoJogo()
        {
            DateTime data = await _jogoRepository.ObterDataUltimoJogo();
            return data;
        }

        public async Task<int> ObterQtdJogosDisputados()
        {
            int qtdJogos = await _jogoRepository.ObterQtdJogosDisputados();
            return qtdJogos;
        }

        public async Task<int> ObterTotalPontosTemporada()
        {
            int totalPontos = await _jogoRepository.ObterTotalPontosTemporada();
            return totalPontos;
        }

        public async Task<int> ObterMediaPontosPorJogo()
        {
            int mediaPontos = await _jogoRepository.ObterMediaPontosPorJogo();
            return mediaPontos;
        }

        public async Task<int> ObterMaiorPontuacaoEmJogo()
        {
            int maiorPontuacao = await _jogoRepository.ObterMaiorPontuacaoEmJogo();
            return maiorPontuacao;
        }

        public async Task<int> ObterMenorPontuacaoEmJogo()
        {
            int menorPontuacao = await _jogoRepository.ObterMenorPontuacaoEmJogo();
            return menorPontuacao;
        }

        public async Task<int> ObterQtdRecordesBatidos()
        {
            int qtdRecordesBatidos = await _jogoRepository.ObterQtdRecordesBatidos();
            return qtdRecordesBatidos;
        }

        public async Task<Resultados> ObterResultadosDosJogos()
        {
            return new Resultados
            {
                DataPrimeiroJogo = await ObterDataPrimeiroJogo(),
                DataUltimoJogo = await ObterDataUltimoJogo(),
                JogosDisputados = await ObterQtdJogosDisputados(),
                TotalPontosTemporada = await ObterTotalPontosTemporada(),
                MediaPontosPorJogo = await ObterMediaPontosPorJogo(),
                MaiorPontuacaoEmJogo = await ObterMaiorPontuacaoEmJogo(),
                MenorPontuacaoEmJogo = await ObterMenorPontuacaoEmJogo(),
                QtdVezesBateuRecorde = await ObterQtdRecordesBatidos()
            };
        }
    }
}
