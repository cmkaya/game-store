using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games;
using GameStore.WebApi.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

const string ConnectionStringKey = "GameStoreDb";
var connectionString = builder.Configuration.GetConnectionString(ConnectionStringKey) ?? 
                       throw new InvalidOperationException(
                         $"Connection string '{ConnectionStringKey}' not found in configuration");

builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGames();
app.MapGenres();
app.InitializeDatabase();

app.Run();