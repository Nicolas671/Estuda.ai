using EstudaAi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstudaAi.Context
{
    public class GestaoDBContext : DbContext
    {

        public GestaoDBContext(DbContextOptions<GestaoDBContext> options)
     : base(options)
        {
        }
        public DbSet<Estudante> Estudantes { get; set; }
        public DbSet<Questao> Questaos { get; set; }
        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<ResultFinal> ResultFinals { get; set; }
        public DbSet<Correcao> Correcaos { get; set; }
        public DbSet<Pontuacao> Pontuacaos { get; set; }


    }
}