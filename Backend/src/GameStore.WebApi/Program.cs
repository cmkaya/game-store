using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games;
using GameStore.WebApi.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GameDataLogger>();
builder.Services.AddScoped<GameStoreData>();

var app = builder.Build();

app.MapGames();
app.MapGenres();

app.Run();