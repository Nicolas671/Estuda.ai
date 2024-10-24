
namespace EstudaAi.Entities
{
    public class Correcao
    {
        public int Id { get; set; }
        public string RespostaQuestao { get; set; }
        public string DificuldadeQuestao { get; set; }
        public Questao questao{ get; set; }
        
        
    }
}