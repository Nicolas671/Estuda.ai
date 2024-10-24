using Microsoft.AspNetCore.Mvc;
using EstudaAi.Context;
using EstudaAi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstudaAi.Controllers
{
    [Route("api/Questoes/[controller]")]
    [ApiController]
    public class QuestaosController : ControllerBase
    {
        private readonly GestaoDBContext _context;

        public QuestaosController(GestaoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questao>>> GetQuestaos()
        {
            return await _context.Questaos.ToListAsync();
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Questao>> GetQuestao(int id)
        {
            var Questao = await _context.Questaos.FindAsync(id);

            if (Questao == null)
            {
                return NotFound();
            }

            return Questao;
        }

      
      
        [HttpPost]
        public async Task<ActionResult<Questao>> PostQuestao(Questao questao)
        {
            _context.Questaos.Add(questao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestao), new { id = questao.Id }, questao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestao(int id, Questao questao)
        {
            if (id != questao.Id)
            {
                return BadRequest("O Id fornecido não corresponde ao Id da questao.");
            }

            _context.Entry(questao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Questao atualizado com sucesso!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestaoExists(id))
                {
                    return NotFound("Questao não encontrado.");
                }
                else
                {
                    throw;
                }
            }
        }


      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestao(int id)
        {
            var questao = await _context.Questaos.FindAsync(id);
            if (questao == null)
            {
                return NotFound();
            }

            _context.Questaos.Remove(questao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestaoExists(int id)
        {
            return _context.Questaos.Any(e => e.Id == id);
        }

        

    }
}