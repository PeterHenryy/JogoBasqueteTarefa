using JogoBasqueteTarefa.Models;
using JogoBasqueteTarefa.Services.Interfaces;
using JogoBasqueteTarefa.Validators;
using Microsoft.AspNetCore.Mvc;

namespace JogoBasqueteTarefa.Controllers
{
    [ApiController]
    [Route("jogo")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogoController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpGet("resultados-jogos")]
        public async Task<IActionResult> ObterResultadosJogos()
        {
            //se não houver jogos, avisar ao front end
            if(await _jogoService.ObterQtdJogosDisputados() == 0)
            {
                return BadRequest(new { message = "Não há nenhum jogo disponível" });
            }

            //retornando objeto contendo informações úteis ao front end
            Resultados resultadosDosJogos = await _jogoService.ObterResultadosDosJogos();
            return Ok(resultadosDosJogos);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(Jogo jogo)
        {
            //Guardando possível erro em variável para avisar o usuário front-end qual é o erro
            string erroAoCriarJogo = JogoValidator.VerificarValidadeJogo(jogo);
            if (!String.IsNullOrEmpty(erroAoCriarJogo))
            {
                return BadRequest(new { message = erroAoCriarJogo });
            }

            try
            {
                bool jogoCriado = await _jogoService.Criar(jogo);
                return CreatedAtAction(nameof(ObterJogoPorID), new { id = jogo.ID }, jogo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Não foi possível criar o jogo: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Jogo>> ObterJogoPorID(int id)
        {
            var jogo = await _jogoService.ObterJogoPorID(id);
            if (jogo == null)
            {
                return NotFound();
            }
            return Ok(jogo);
        }
    }
}
