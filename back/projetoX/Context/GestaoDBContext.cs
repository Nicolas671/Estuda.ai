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



    }
}