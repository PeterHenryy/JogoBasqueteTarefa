using System.ComponentModel.DataAnnotations;

namespace JogoBasqueteTarefa.Models
{
    public class Jogo
    {
        [Key]
        public int ID { get; set; }

        public DateTime Data { get; set; }
        public int Pontos { get; set; }
    }
}
