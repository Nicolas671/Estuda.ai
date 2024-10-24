using Microsoft.AspNetCore.Mvc;
using EstudaAi.Context;
using EstudaAi.Entities;
 
 
namespace EstudaAi.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly GestaoDBContext _context;
 
        public AutenticacaoController(GestaoDBContext dbContext)
        {
            _context = dbContext;
        }
 
        [HttpPost("login")]
        public IActionResult Login([FromBody] Estudante estudante)
        {
            var user = _context.Estudantes.FirstOrDefault(u => u.Email == estudante.Email && u.Senha == estudante.Senha);
            if (user == null)
            {
                return Unauthorized();
            }
         
            return Ok(new { Message = "Logado!", UsuarioId = estudante.Id });
        }
    }
}