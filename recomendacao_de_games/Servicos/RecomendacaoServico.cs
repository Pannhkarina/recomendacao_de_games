using Newtonsoft.Json;
using recomendacao_de_games.Data;
using recomendacao_de_games.Models;

namespace recomendacao_de_games.Servicos
{
    public class RecomendacaoServico
    {

        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public RecomendacaoServico(AppDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }
        public async Task<JogosModel?> ObterRecomendacaoAsync(FiltroUsuarioDTO filtrosUsuario)
        {
            if (filtrosUsuario.Generos == null || !filtrosUsuario.Generos.Any())
                throw new ArgumentException("Pelo menos um gênero deve ser informado.");

            var json = await _httpClient.GetStringAsync("https://www.freetogame.com/api/games");
            var jogos = JsonConvert.DeserializeObject<List<dynamic>>(json);

            if (jogos == null || jogos.Count == 0)
                return null;

            var filtrados = jogos
                .Where(j => filtrosUsuario.Generos.Any(g => ((string)j["genre"]).Contains(g, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            if (!string.IsNullOrEmpty(filtrosUsuario.Plataforma) && filtrosUsuario.Plataforma.ToLower() != "ambos")
            {
                filtrados = filtrados
                    .Where(j => ((string)j["platform"]).ToLower().Contains(filtrosUsuario.Plataforma.ToLower()))
                    .ToList();
            }
            if (filtrosUsuario.RamDisponivel.HasValue && filtrosUsuario.RamDisponivel < 4)
            {
                filtrados = filtrados.Where(j => !((string)j["title"]).Contains("3D")).ToList();
            }

            if (!filtrados.Any())
                return null;

            var random = new Random();
            var escolhido = filtrados[random.Next(filtrados.Count)];

            var jogo = new JogosModel
            {
                Titulo = escolhido["title"],
                Categoria = escolhido["genre"]
            };

            // Salva no banco
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();

            return jogo;
        }

        public IEnumerable<JogosModel> ListarRecomendacoes()
        {
            return _context.Jogos
                .OrderByDescending(j => j.DataRecomendacao)
                .ToList();
        }

           
    }
}
