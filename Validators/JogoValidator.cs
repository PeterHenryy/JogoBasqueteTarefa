using JogoBasqueteTarefa.Models;
using System.ComponentModel.DataAnnotations;

namespace JogoBasqueteTarefa.Validators
{
    public class JogoValidator
    {
        public static bool VerificarValidadeJogo(Jogo jogo)
        {
            if(jogo.Data > DateTime.Now)
            {
                return false;
            }
            if(jogo.Pontos <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
