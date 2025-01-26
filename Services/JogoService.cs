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

        public bool Criar(Jogo jogo)
        {
            bool jogoCriado = _jogoRepository.Criar(jogo);
            return jogoCriado;
        }


        public Jogo ObterJogoPorID(int jogoID)
        {
            Jogo jogo = _jogoRepository.ObterJogoPorID(jogoID);
            return jogo;
        }

        public DateTime ObterDataPrimeiroJogo()
        {
            DateTime data = _jogoRepository.ObterDataPrimeiroJogo();
            return data;
        }

        public DateTime ObterDataUltimoJogo()
        {
            DateTime data = _jogoRepository.ObterDataUltimoJogo();
            return data;
        }

        public int ObterQtdJogosDisputados()
        {
            int qtdJogos = _jogoRepository.ObterQtdJogosDisputados();
            return qtdJogos;
        }

        public int ObterTotalPontosTemporada()
        {
            int totalPontos = _jogoRepository.ObterTotalPontosTemporada();
            return totalPontos;
        }

        public int ObterMediaPontosPorJogo()
        {
            int mediaPontos = _jogoRepository.ObterMediaPontosPorJogo();
            return mediaPontos;
        }

        public int ObterMaiorPontuacaoEmJogo()
        {
            int maiorPontuacao = _jogoRepository.ObterMaiorPontuacaoEmJogo();
            return maiorPontuacao;
        }

        public int ObterMenorPontuacaoEmJogo()
        {
            int menorPontuacao = _jogoRepository.ObterMenorPontuacaoEmJogo();
            return menorPontuacao;
        }

        public int ObterQtdRecordesBatidos()
        {
            int qtdRecordesBatidos = _jogoRepository.ObterQtdRecordesBatidos();
            return qtdRecordesBatidos;
        }

        public Resultados ObterResultadosDosJogos()
        {
            return new Resultados
            {
                DataPrimeiroJogo = ObterDataPrimeiroJogo(),
                DataUltimoJogo = ObterDataUltimoJogo(),
                JogosDisputados = ObterQtdJogosDisputados(),
                TotalPontosTemporada = ObterTotalPontosTemporada(),
                MediaPontosPorJogo = ObterMediaPontosPorJogo(),
                MaiorPontuacaoEmJogo = ObterMaiorPontuacaoEmJogo(),
                MenorPontuacaoEmJogo = ObterMenorPontuacaoEmJogo(),
                QtdVezesBateuRecorde = ObterQtdRecordesBatidos()
            };
        }
    }
}
