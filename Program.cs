
using JogoBasqueteTarefa.Data;
using JogoBasqueteTarefa.Repositories;
using JogoBasqueteTarefa.Services;
using Microsoft.EntityFrameworkCore;

namespace JogoBasqueteTarefa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("String de conex?o n?o encontrada");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddControllers();
            builder.Services.AddTransient<JogoRepository>();
            builder.Services.AddTransient<JogoService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
