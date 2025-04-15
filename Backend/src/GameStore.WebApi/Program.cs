using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games.DeleteGame;
using GameStore.WebApi.Features.Games.GetGame;
using GameStore.WebApi.Features.Games.GetGames;
using GameStore.WebApi.Features.Games.PostGame;
using GameStore.WebApi.Features.Games.UpdateGames;
using GameStore.WebApi.Features.Genres.GetGenres;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

GameStoreData data = new();

app.MapGetGames(data);
app.MapGetGame(data);
app.MapUpdateGame(data);
app.MapPostGame(data);
app.MapDeleteGame(data);

app.MapGetGenres(data);

app.Run();