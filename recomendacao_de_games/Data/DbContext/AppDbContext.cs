using Microsoft.EntityFrameworkCore;
using recomendacao_de_games.Models;

namespace recomendacao_de_games.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

            //_configurationAppSettings = configurationAppSettings;

        }

        public DbSet<JogosModel> Jogos { get; set; }

    }
}
