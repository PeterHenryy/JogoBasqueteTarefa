using JogoBasqueteTarefa.Validators;
using System.ComponentModel.DataAnnotations;

namespace JogoBasqueteTarefa.Models
{
    public class Jogo
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O jogo precisa de uma data!")]
        public DateTime Data { get; set; }

        [Range(1, 1000, ErrorMessage = "Pontos devem ser maiores que 0.")]
        public int Pontos { get; set; }
    }
}
