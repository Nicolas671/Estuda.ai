using Microsoft.AspNetCore.Mvc;
using EstudaAi.Context;
using EstudaAi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstudaAi.Controllers
{
    [Route("api/Estudantes/[controller]")]
    [ApiController]
    public class EstudantesController : ControllerBase
    {
        private readonly GestaoDBContext _context;

        public EstudantesController(GestaoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudante>>> GetEstudantes()
        {
            return await _context.Estudantes.ToListAsync();
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudante>> GetEstudante(int id)
        {
            var estudante = await _context.Estudantes.FindAsync(id);

            if (estudante == null)
            {
                return NotFound();
            }

            return estudante;
        }

      
      
        [HttpPost]
        public async Task<ActionResult<Estudante>> PostEstudante(Estudante estudante)
        {
            _context.Estudantes.Add(estudante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstudante), new { id = estudante.Id }, estudante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudante(int id, Estudante estudante)
        {
            if (id != estudante.Id)
            {
                return BadRequest("O Id fornecido não corresponde ao Id do cliente.");
            }

            _context.Entry(estudante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Estudante atualizado com sucesso!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudanteExists(id))
                {
                    return NotFound("Estudante não encontrado.");
                }
                else
                {
                    throw;
                }
            }
        }


      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudante(int id)
        {
            var estudante = await _context.Estudantes.FindAsync(id);
            if (estudante == null)
            {
                return NotFound();
            }

            _context.Estudantes.Remove(estudante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstudanteExists(int id)
        {
            return _context.Estudantes.Any(e => e.Id == id);
        }
    }
}
