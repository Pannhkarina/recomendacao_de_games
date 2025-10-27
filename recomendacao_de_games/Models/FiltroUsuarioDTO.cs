namespace recomendacao_de_games.Models
{
    public class FiltroUsuarioDTO
    {
        public List<string> Generos { get; set; } = new();
        public string? Plataforma { get; set; } 
        public int? RamDisponivel { get; set; } 
    }
}
