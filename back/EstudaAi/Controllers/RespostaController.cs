using Microsoft.AspNetCore.Mvc;
using EstudaAi.Context;
using EstudaAi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstudaAi.Controllers
{
    [Route("api/Respostas/[controller]")]
    [ApiController]
    public class RespostasController : ControllerBase
    {
        private readonly GestaoDBContext _context;

        public RespostasController(GestaoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resposta>>> GetRespostas()
        {
            return await _context.Respostas.ToListAsync();
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Resposta>> GetResposta(int id)
        {
            var resposta = await _context.Respostas.FindAsync(id);

            if (resposta == null)
            {
                return NotFound();
            }

            return resposta;
        }
      
        [HttpPost]
        public async Task<ActionResult<Resposta>> PostResposta(Resposta resposta)
        {
            if (resposta == null)
            {
                return BadRequest("Dados inválidos para criar uma correcao.");
            }

         //   var questaoExistente = await _context.Questaos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == correcao.Id);
            var QuestaoExistente = await _context.Questaos.FindAsync(resposta.questao.Id);
          
            if (QuestaoExistente == null)
            {
                return BadRequest("Cliente não encontrado. O pedido deve estar associado a um cliente existente.");
            }

            var EstudanteExistente = await _context.Estudantes.FindAsync(resposta.estudante.Id);
          
            if (EstudanteExistente == null)
            {
                return BadRequest("Questao não encontrada. O pedido deve estar associado a um questao existente.");
            }

            resposta.questao = QuestaoExistente;
            resposta.estudante = EstudanteExistente;

            _context.Respostas.Add(resposta);
           
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResposta), new {id = resposta.Id}, resposta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutResposta(int id, Resposta resposta)
        {
            if (id != resposta.Id)
            {
                return BadRequest("O Id fornecido não corresponde ao Id do cliente.");
            }

            _context.Entry(resposta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Estudante atualizado com sucesso!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespostaExists(id))
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
        public async Task<IActionResult> DeleteResposta(int id)
        {
            var resposta = await _context.Respostas.FindAsync(id);
            if (resposta == null)
            {
                return NotFound();
            }

            _context.Respostas.Remove(resposta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RespostaExists(int id)
        {
            return _context.Respostas.Any(e => e.Id == id);
        }
    }
}
