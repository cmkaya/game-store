using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games;
using GameStore.WebApi.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

const string ConnectionString = "Data Source=GameStore.db";

builder.Services.AddSqlite<GameStoreContext>(ConnectionString);
builder.Services.AddTransient<GameDataLogger>();
builder.Services.AddSingleton<GameStoreData>();

var app = builder.Build();

app.MapGames();
app.MapGenres();

app.Run();