using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games;
using GameStore.WebApi.Features.Genres.GetGenres;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

GameStoreData data = new();

app.MapGames(data);

app.MapGenres(data);

app.Run();