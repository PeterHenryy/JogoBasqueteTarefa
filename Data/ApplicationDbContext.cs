using JogoBasqueteTarefa.Models;
using Microsoft.EntityFrameworkCore;

namespace JogoBasqueteTarefa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Jogo> Jogos { get; set; }

    }
}
