using System.ComponentModel.DataAnnotations;
using GameStore.WebApi.Data;
using GameStore.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameByIdRoute = "GetGameById";

GameStoreData data = new();

app.MapGet("/games", () =>
  data.GetGames().Select(game => new GameSummaryDto(
    game.Id,
    game.Title,
    game.Genre.Name,
    game.Price,
    game.ReleaseDate
  ))
);

app.MapGet("/games/{id:guid}", (Guid id) =>
{
  var game = data.GetGame(id);

  return game is null
    ? Results.NotFound()
    : Results.Ok(new GameDetailsDto(
      game.Id,
      game.Title,
      game.Genre.Id,
      game.Price,
      game.ReleaseDate,
      game.Description
    ));
}).WithName(GetGameByIdRoute);

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

app.MapPut("/games/{id:guid}", (Guid id, UpdateGameDto gameDto) =>
{
  var existingGame = data.GetGame(id);
  if (existingGame is null)
  {
    return Results.NotFound();
  }

  var genre = data.GetGenre(gameDto.GenreId);
  if (genre is null)
  {
    return Results.BadRequest("Invalid genre Id.");
  }

  existingGame.Title = gameDto.Title;
  existingGame.Genre = genre;
  existingGame.Price = gameDto.Price;
  existingGame.ReleaseDate = gameDto.ReleaseDate;
  existingGame.Description = gameDto.Description;

  return Results.NoContent();
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

public record GameDetailsDto(
  Guid Id,
  string Title,
  Guid GenreId,
  decimal Price,
  DateOnly ReleaseDate,
  string Description
);

public record GameSummaryDto(
  Guid Id,
  string Title,
  string Genre,
  decimal Price,
  DateOnly ReleaseDate
);

public record CreateGameDto(
  [Required]
  [StringLength(50, MinimumLength = 3)]
  string Title,
  Guid GenreId,
  [Range(0.01, 100.00)] decimal Price,
  DateOnly ReleaseDate,
  [Required][StringLength(500)] string Description
);

public record UpdateGameDto(
  [Required]
  [StringLength(50, MinimumLength = 3)]
  string Title,
  Guid GenreId,
  [Range(0.01, 100.00)] decimal Price,
  DateOnly ReleaseDate,
  [Required][StringLength(500)] string Description
);

public record GenreDto(Guid Id, string Name);