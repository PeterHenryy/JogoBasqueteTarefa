using JogoBasqueteTarefa.Models;
using JogoBasqueteTarefa.Services;
using Microsoft.AspNetCore.Mvc;

namespace JogoBasqueteTarefa.Controllers
{
    [ApiController]
    [Route("jogo")]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _jogoService;

        public JogoController(JogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpGet("resultados-jogos")]
        public async Task<IActionResult> ObterResultadosJogos()
        {
            if(await _jogoService.ObterQtdJogosDisputados() == 0)
            {
                return BadRequest(new { message = "Não há nenhum jogo disponível" });
            }
            Resultados resultadosDosJogos = await _jogoService.ObterResultadosDosJogos();
            return Ok(resultadosDosJogos);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(Jogo jogo)
        {
            if (jogo.Data > DateTime.Now)
            {
                return BadRequest(new { message = "A data do jogo não pode ser depois da data atual." });
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
