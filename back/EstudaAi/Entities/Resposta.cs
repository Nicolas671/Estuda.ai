namespace EstudaAi.Entities
{
    public class Resposta
    {
        public int Id { get; set; }
        public string RespostaUser { get; set; }
        public Questao questao{ get; set; }
        public Estudante estudante{ get; set; }
        
    }
}