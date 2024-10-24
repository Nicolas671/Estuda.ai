using Microsoft.AspNetCore.Mvc;
using EstudaAi.Context;
using EstudaAi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstudaAi.Controllers
{
    [Route("api/Registros/[controller]")]
    [ApiController]
    public class RegistrosController : ControllerBase
    {
        private readonly GestaoDBContext _context;

        public RegistrosController(GestaoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registro>>> GetRegistro()
        {
            return await _context.Registros.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Registro>> GetRegistro(int id)
        {
            var registro = await _context.Registros.FindAsync(id);

            if (registro == null)
            {
                return NotFound();
            }

            return registro;
        }

        [HttpPost]
        public async Task<IActionResult> PostRegistro(Registro registro)
        {
            if (registro == null)
            {
                return BadRequest("Dados inválidos para criar uma correcao.");
            }

         //   var questaoExistente = await _context.Questaos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == correcao.Id);
            var PontuacaoExistente = await _context.Pontuacaos.FindAsync(registro.pontuacao.Id);
          
            if (PontuacaoExistente == null)
            {
                return BadRequest("Cliente não encontrado. O pedido deve estar associado a um cliente existente.");
            }

            var EstudanteExistente = await _context.Estudantes.FindAsync(registro.estudante.Id);
          
            if (EstudanteExistente == null)
            {
                return BadRequest("Questao não encontrada. O pedido deve estar associado a um questao existente.");
            }

            registro.pontuacao = PontuacaoExistente;
            registro.estudante = EstudanteExistente;

            _context.Registros.Add(registro);
           
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistro), new {id = registro.Id}, registro);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistro(int id, Registro registro)
        {
            if (id != registro.Id)
            {
                return BadRequest("O Id fornecido não corresponde ao Id do registro.");
            }

            _context.Entry(registro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Registro atualizado com sucesso!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroExists(id))
                {
                    return NotFound("Registro não encontrado.");
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistro(int id)
        {
            var registro = await _context.Registros.FindAsync(id);
            if (registro == null)
            {
                return NotFound();
            }

            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistroExists(int id)
        {
            return _context.Registros.Any(e => e.Id == id);
        }
    }
}