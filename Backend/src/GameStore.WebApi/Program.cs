using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games.GetGame;
using GameStore.WebApi.Features.Games.GetGames;
using GameStore.WebApi.Features.Games.PostGame;
using GameStore.WebApi.Features.Games.UpdateGames;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

GameStoreData data = new();

app.MapGetGames(data);
app.MapGetGame(data);
app.MapUpdateGame(data);
app.MapPostGame(data);

app.MapDelete("/games/{id:guid}", (Guid id) =>
{
  data.RemoveGame(id);

  return Results.NoContent();
});

app.MapGet("/genres", () =>
  data.GetGenres().Select(genre => new GenreDto(genre.Id, genre.Name))
);

app.Run();

public record GenreDto(Guid Id, string Name);