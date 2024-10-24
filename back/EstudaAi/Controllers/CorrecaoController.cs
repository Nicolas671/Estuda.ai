using Microsoft.AspNetCore.Mvc;
using EstudaAi.Context;
using EstudaAi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstudaAi.Controllers
{
    [Route("api/Correcaos/[controller]")]
    [ApiController]
    public class CorrecaosController : ControllerBase
    {
        private readonly GestaoDBContext _context;

        public CorrecaosController(GestaoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Correcao>>> GetCorrecaos()
        {
            return await _context.Correcaos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Correcao>> GetCorrecao(int id)
        {
            var correcao = await _context.Correcaos.FindAsync(id);

            if (correcao == null)
            {
                return NotFound();
            }

            return correcao;
        }

        [HttpPost]
        public async Task<IActionResult> PostCorrecao(Correcao correcao)
        {
            if (correcao == null)
            {
                return BadRequest("Dados inválidos para criar uma correcao.");
            }

         //   var questaoExistente = await _context.Questaos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == correcao.Id);
            var questaoExistente = await _context.Questaos.FindAsync(correcao.questao.Id);
          
            if (questaoExistente == null)
            {
                return BadRequest("Questao não encontrada. O pedido deve estar associado a um questao existente.");
            }

            correcao.questao = questaoExistente;
            _context.Correcaos.Add(correcao);
           
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCorrecao), new {id = correcao.Id}, correcao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorrecao(int id, Correcao correcao)
        {
            if (id != correcao.Id)
            {
                return BadRequest("O Id fornecido não corresponde ao Id da correção.");
            }

            _context.Entry(correcao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Correção atualizada com sucesso!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorrecaoExists(id))
                {
                    return NotFound("Correção não encontrada.");
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorrecao(int id)
        {
            var correcao = await _context.Correcaos.FindAsync(id);
            if (correcao == null)
            {
                return NotFound();
            }

            _context.Correcaos.Remove(correcao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CorrecaoExists(int id)
        {
            return _context.Correcaos.Any(e => e.Id == id);
        }
    }
}