using JogoBasqueteTarefa.Models;

namespace JogoBasqueteTarefa.Repositories
{
    public interface IJogoRepository
    {
        bool Criar(Jogo jogo);
        IEnumerable<Jogo> ObterJogos();
        Jogo ObterJogoPorID(int jogoID);
    }
}
