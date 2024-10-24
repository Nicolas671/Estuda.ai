namespace EstudaAi.Entities
{
    public class Registro
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public Estudante estudante { get; set; }
        public Pontuacao pontuacao{ get; set; }
        
    }
}