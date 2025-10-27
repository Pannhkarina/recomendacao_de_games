# recomendacao_de_games


## Pré-requisitos
* NET 8 SDK
* SQL Server
* Visual Studio ou VS Code
* Pacotes NuGet:
  > Microsoft.EntityFrameworkCore.SqlServer
  > Microsoft.EntityFrameworkCore.Design

## Configuração do Banco de Dados
* No arquivo appsettings.json, configure a connection string.

## Criando o Banco com Entity Framework
* Add-Migration InitialCreate
* Update-Database

## Executando a API Localmente
1. No terminal, acesse o diretório do projeto:
> cd MinimalApi
2. Execute a API
> dotnet run
3. Acesse o navegador

## Endpoints da API
POST /api/recomendar/
> {
  "generos": ["string"],
  "plataforma": "string",
  "ramDisponivel": 0
}
> 
GET /api/historico
