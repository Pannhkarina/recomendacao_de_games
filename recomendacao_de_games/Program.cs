using Microsoft.EntityFrameworkCore;
using recomendacao_de_games.Data;
using recomendacao_de_games.Models;
using recomendacao_de_games.Servicos;




#region Builders   

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<RecomendacaoServico>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
#endregion



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Recomendacao de forma aleatória
app.MapPost("/api/recomendar", async (FiltroUsuarioDTO filtros, RecomendacaoServico service) =>
{
    try
    {
        var jogo = await service.ObterRecomendacaoAsync(filtros);
        if (jogo == null)
            return Results.NotFound(new { mensagem = "Nenhum jogo encontrado com os critérios informados." });

        return Results.Ok(new { jogo.Titulo, jogo.Categoria });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { erro = ex.Message });
    }
})
.WithName("RecomendarJogo")
.WithOpenApi();

// historico de recomendação
app.MapGet("/api/historico", (RecomendacaoServico service) =>
{
    var jogos = service.ListarRecomendacoes();
    return Results.Ok(jogos);
})
.WithName("ListarHistorico")
.WithOpenApi();




app.Run();

