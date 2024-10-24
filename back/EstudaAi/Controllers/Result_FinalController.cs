using Microsoft.AspNetCore.Mvc;
using EstudaAi.Context;
using EstudaAi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstudaAi.Controllers
{
    [Route("api/ResultFinals/[controller]")]
    [ApiController]
    public class ResultFinalsController : ControllerBase
    {
        private readonly GestaoDBContext _context;

        public ResultFinalsController(GestaoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultFinal>>> GetResultFinals()
        {
            return await _context.ResultFinals.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultFinal>> GetResultFinal(int id)
        {
            var resultFinal = await _context.ResultFinals.FindAsync(id);

            if (resultFinal == null)
            {
                return NotFound();
            }

            return resultFinal;
        }

        [HttpPost]
        public async Task<ActionResult<ResultFinal>> PostResultFinal(ResultFinal resultFinal)
        {
            if (resultFinal == null)
            {
                return BadRequest("Dados inválidos para criar uma correcao.");
            }

         //   var questaoExistente = await _context.Questaos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == correcao.Id);
            var CorrecaoExistente = await _context.Correcaos.FindAsync(resultFinal.correcao.Id);
          
            if (CorrecaoExistente == null)
            {
                return BadRequest("Cliente não encontrado. O pedido deve estar associado a um cliente existente.");
            }

            var RespostaExistente = await _context.Respostas.FindAsync(resultFinal.resposta.Id);
          
            if (RespostaExistente == null)
            {
                return BadRequest("Questao não encontrada. O pedido deve estar associado a um questao existente.");
            }

            resultFinal.correcao = CorrecaoExistente;
            resultFinal.resposta = RespostaExistente;

            _context.ResultFinals.Add(resultFinal);
           
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResultFinal), new {id = resultFinal.Id}, resultFinal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutResultFinal(int id, ResultFinal resultFinal)
        {
            if (id != resultFinal.Id)
            {
                return BadRequest("O Id fornecido não corresponde ao Id do resultado final.");
            }

            _context.Entry(resultFinal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Resultado final atualizado com sucesso!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultFinalExists(id))
                {
                    return NotFound("Resultado final não encontrado.");
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResultFinal(int id)
        {
            var resultFinal = await _context.ResultFinals.FindAsync(id);
            if (resultFinal == null)
            {
                return NotFound();
            }

            _context.ResultFinals.Remove(resultFinal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultFinalExists(int id)
        {
            return _context.ResultFinals.Any(e => e.Id == id);
        }
    }
}
