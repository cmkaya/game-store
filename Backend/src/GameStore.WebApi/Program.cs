using System.ComponentModel.DataAnnotations;
using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games.DeleteGame;
using GameStore.WebApi.Features.Games.GetGame;
using GameStore.WebApi.Features.Games.GetGames;
using GameStore.WebApi.Features.Games.PostGame;
using GameStore.WebApi.Features.Games.UpdateGames;
using GameStore.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameByIdRoute = "GetGameById";

GameStoreData data = new();

app.MapGetGames(data);
app.MapGetGame(data);
app.MapUpdateGame(data);

app.MapPost("/games", (CreateGameDto gameDto) =>
{
  var genre = data.GetGenre(gameDto.GenreId);
  if (genre is null)
  {
    return Results.BadRequest("Invalid genre Id.");
  }

  var game = new Game
  {
    Title = gameDto.Title,
    Genre = genre,
    Price = gameDto.Price,
    ReleaseDate = gameDto.ReleaseDate,
    Description = gameDto.Description
  };

  data.AddGame(game);

  return Results.CreatedAtRoute(GetGameByIdRoute, new { id = game.Id }, new GameDetailsDto(
    game.Id,
    game.Title,
    game.Genre.Id,
    game.Price,
    game.ReleaseDate,
    game.Description
  ));
}).WithParameterValidation();

app.MapDelete("/games/{id:guid}", (Guid id) =>
{
  data.RemoveGame(id);

  return Results.NoContent();
});

app.MapGet("/genres", () =>
  data.GetGenres().Select(genre => new GenreDto(genre.Id, genre.Name))
);

app.Run();

public record CreateGameDto(
  [Required]
  [StringLength(50, MinimumLength = 3)]
  string Title,
  Guid GenreId,
  [Range(0.01, 100.00)] decimal Price,
  DateOnly ReleaseDate,
  [Required][StringLength(500)] string Description
);

public record GenreDto(Guid Id, string Name);