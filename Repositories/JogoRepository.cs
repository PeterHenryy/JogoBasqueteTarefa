using JogoBasqueteTarefa.Data;
using JogoBasqueteTarefa.Models;
using JogoBasqueteTarefa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JogoBasqueteTarefa.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly ApplicationDbContext _context;

        public JogoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Criar(Jogo jogo)
        {
            try
            {
                await _context.Jogos.AddAsync(jogo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<Jogo> ObterJogoPorID(int jogoID)
        {
            Jogo jogo = await _context.Jogos.SingleOrDefaultAsync(x => x.ID == jogoID);
            return jogo;
        }

        public async Task<DateTime> ObterDataPrimeiroJogo()
        {
            Jogo primeiroJogo = await _context.Jogos.OrderBy(x => x.Data).FirstOrDefaultAsync();
            return primeiroJogo.Data;
        }

        public async Task<DateTime> ObterDataUltimoJogo()
        {
            Jogo ultimoJogo = await _context.Jogos.OrderByDescending(x => x.Data).FirstOrDefaultAsync();
            return ultimoJogo.Data;
        }

        public async Task<int> ObterQtdJogosDisputados()
        {
            int jogosDisputados = await _context.Jogos.CountAsync();
            return jogosDisputados;
        }

        public async Task<int> ObterTotalPontosTemporada()
        {
            int totalPontos = await _context.Jogos.SumAsync(x => x.Pontos);
            return totalPontos;
        }

        public async Task<int> ObterMediaPontosPorJogo()
        {
            int media = (int) await _context.Jogos.AverageAsync(x => x.Pontos);
            return media;
        }

        public async Task<int> ObterMaiorPontuacaoEmJogo()
        {
            int maiorPontuacao = await _context.Jogos.MaxAsync(x => x.Pontos);
            return maiorPontuacao;
        }

        public async Task<int> ObterMenorPontuacaoEmJogo()
        {
            int menorPontuacao = await _context.Jogos.MinAsync(x => x.Pontos);
            return menorPontuacao;
        }

        public async Task<int> ObterQtdRecordesBatidos()
        {
            Jogo primeiroJogo = await _context.Jogos.ElementAtAsync(0);
            int recordeAtual = primeiroJogo.Pontos;
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
