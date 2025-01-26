using JogoBasqueteTarefa.Data;
using JogoBasqueteTarefa.Models;
using JogoBasqueteTarefa.Repositories.Interfaces;

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


        public Jogo ObterJogoPorID(int jogoID)
        {
            Jogo jogo = _context.Jogos.Single(x => x.ID == jogoID);
            return jogo;
        }

        public DateTime ObterDataPrimeiroJogo()
        {
            DateTime data = _context.Jogos.OrderBy(x => x.Data).FirstOrDefault().Data;
            return data;
        }

        public DateTime ObterDataUltimoJogo()
        {
            DateTime data = _context.Jogos.OrderByDescending(x => x.Data).FirstOrDefault().Data;
            return data;
        }

        public int ObterQtdJogosDisputados()
        {
            int jogosDisputados = _context.Jogos.Count();
            return jogosDisputados;
        }

        public int ObterTotalPontosTemporada()
        {
            int totalPontos = _context.Jogos.Sum(x => x.Pontos);
            return totalPontos;
        }

        public int ObterMediaPontosPorJogo()
        {
            int media = (int)_context.Jogos.Average(x => x.Pontos);
            return media;
        }

        public int ObterMaiorPontuacaoEmJogo()
        {
            int maiorPontuacao = _context.Jogos.Max(x => x.Pontos);
            return maiorPontuacao;
        }

        public int ObterMenorPontuacaoEmJogo()
        {
            int menorPontuacao = _context.Jogos.Min(x => x.Pontos);
            return menorPontuacao;
        }

        public int ObterQtdRecordesBatidos()
        {
            int recordeAtual = _context.Jogos.ElementAt(0).Pontos;
            int recordesBatidos = 0;
            foreach(Jogo jogo in _context.Jogos)
            {
                if (jogo.Pontos > recordeAtual)
                {
                    recordesBatidos++;
                    recordeAtual = jogo.Pontos;
                }
            }
            return recordesBatidos;
        }
    }
}
