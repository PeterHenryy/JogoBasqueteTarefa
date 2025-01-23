using JogoBasqueteTarefa.Models;
using JogoBasqueteTarefa.Repositories;

namespace JogoBasqueteTarefa.Services
{
    public class JogoService
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

        public IEnumerable<Jogo> ObterJogos()
        {
            List<Jogo> jogos = _jogoRepository.ObterJogos().ToList();
            return jogos;
        }

        public Jogo ObterJogoPorID(int jogoID)
        {
            Jogo jogo = _jogoRepository.ObterJogoPorID(jogoID);
            return jogo;
        }
    }
}
