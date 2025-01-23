using JogoBasqueteTarefa.Data;
using JogoBasqueteTarefa.Models;

namespace JogoBasqueteTarefa.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly ApplicationDbContext _context;

        public JogoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Criar(Jogo jogo)
        {
            try
            {
                _context.Jogos.Add(jogo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Jogo> ObterJogos()
        {
            return _context.Jogos.ToList();
        }

        public Jogo ObterJogoPorID(int jogoID)
        {
            Jogo jogo = _context.Jogos.Single(x => x.ID == jogoID);
            return jogo;
        }
    }
}
