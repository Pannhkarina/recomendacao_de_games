namespace recomendacao_de_games.Models
{
    public class JogosModel
    {

        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public DateTime DataRecomendacao { get; set; } = DateTime.Now;
    }
}
