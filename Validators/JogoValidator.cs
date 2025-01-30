using JogoBasqueteTarefa.Models;
using System.ComponentModel.DataAnnotations;

namespace JogoBasqueteTarefa.Validators
{
    public class JogoValidator
    {
        public static string VerificarValidadeJogo(Jogo jogo)
        {
            if(jogo.Data > DateTime.Now)
            {
                return "A data do jogo não pode estar no futuro!";
            }
            if(jogo.Pontos <= 0)
            {
                return "O jogo deve ter pontuação maior que 0!";
            }
            return "";
        }
    }
}
