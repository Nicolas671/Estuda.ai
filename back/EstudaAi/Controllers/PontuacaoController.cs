using Microsoft.AspNetCore.Mvc;
using EstudaAi.Context;
using EstudaAi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstudaAi.Controllers
{
    [Route("api/Pontuacaos/[controller]")]
    [ApiController]
    public class PontuacaosController : ControllerBase
    {
        private readonly GestaoDBContext _context;

        public PontuacaosController(GestaoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pontuacao>>> GetPontuacaos()
        {
            return await _context.Pontuacaos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pontuacao>> GetPontuacao(int id)
        {
            var pontuacao = await _context.Pontuacaos.FindAsync(id);

            if (pontuacao == null)
            {
                return NotFound();
            }

            return pontuacao;
        }

        [HttpPost]
        public async Task<IActionResult> PostPontuacao(Pontuacao pontuacao)
        {
            if (pontuacao == null)
            {
                return BadRequest("Dados inválidos para criar uma correcao.");
            }

         //   var questaoExistente = await _context.Questaos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == correcao.Id);
           var resultFinalExistente = await _context.ResultFinals.FindAsync(pontuacao.resultFinal.Id);
          
            if (resultFinalExistente == null)
            {
                return BadRequest("Cliente não encontrado. O pedido deve estar associado a um cliente existente.");
            }

            pontuacao.resultFinal = resultFinalExistente;
            _context.Pontuacaos.Add(pontuacao);
           
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPontuacao), new {id = pontuacao.Id}, pontuacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPontuacao(int id, Pontuacao pontuacao)
        {
            if (id != pontuacao.Id)
            {
                return BadRequest("O Id fornecido não corresponde ao Id da pontuação.");
            }

            _context.Entry(pontuacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Pontuação atualizada com sucesso!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PontuacaoExists(id))
                {
                    return NotFound("Pontuação não encontrada.");
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePontuacao(int id)
        {
            var pontuacao = await _context.Pontuacaos.FindAsync(id);
            if (pontuacao == null)
            {
                return NotFound();
            }

            _context.Pontuacaos.Remove(pontuacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PontuacaoExists(int id)
        {
            return _context.Pontuacaos.Any(e => e.Id == id);
        }
    }
}