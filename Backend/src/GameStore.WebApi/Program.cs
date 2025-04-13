using System.ComponentModel.DataAnnotations;
using GameStore.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameByIdRoute = "GetGameById";

List<Genre> allGenres =
[
  new Genre { Id = Guid.NewGuid(), Name = "Action-adventure" },
  new Genre { Id = Guid.NewGuid(), Name = "Platformer" },
  new Genre { Id = Guid.NewGuid(), Name = "Action RPG" },
];

List<Game> allGames =
[
  new Game
  {
    Id = Guid.NewGuid(),
    Title = "The Legend of Zelda: Breath of the Wild",
    Genre = allGenres[0],
    Price = 59.99m,
    ReleaseDate = new DateOnly(2017, 3, 3),
    Description = "An open-world action-adventure game set in the vast kingdom of Hyrule."
  },
  new Game
  {
    Id = Guid.NewGuid(),
    Title = "Super Mario Odyssey",
    Genre = allGenres[1],
    Price = 59.99m,
    ReleaseDate = new DateOnly(2017, 10, 27),
    Description = "A 3D platformer where Mario travels across various kingdoms to rescue Princess Peach."
  },
  new Game
  {
    Id = Guid.NewGuid(),
    Title = "The Witcher 3: Wild Hunt",
    Genre = allGenres[2],
    Price = 39.99m,
    ReleaseDate = new DateOnly(2015, 5, 19),
    Description = "An open-world RPG where players control Geralt of Rivia, a monster hunter, in a richly detailed fantasy world."
  }
];

app.MapGet("/games", () => allGames.Select(game =>
  new GameSummaryDto(
  game.Id,
  game.Title,
  game.Genre.Name,
  game.Price,
  game.ReleaseDate
  )
));

app.MapGet("/games/{id:guid}", (Guid id) =>
{
  var game = allGames.Find(g => g.Id == id);

  return game is null ? Results.NotFound() : Results.Ok(
    new GameDetailsDto(
      game.Id,
      game.Title,
      game.Genre.Id,
      game.Price,
      game.ReleaseDate,
      game.Description
    )
  );
})
.WithName(GetGameByIdRoute);

app.MapPost("/games", (CreateGameDto gameDto) =>
{
  var genre = allGenres.Find(genre => genre.Id == gameDto.GenreId);

  if (genre is null)
  {
    return Results.BadRequest("Invalid genre Id.");
  }

  var game = new Game
  {
    Id = Guid.NewGuid(),
    Title = gameDto.Title,
    Genre = genre,
    Price = gameDto.Price,
    ReleaseDate = gameDto.ReleaseDate,
    Description = gameDto.Description
  };

  allGames.Add(game);

  return Results.CreatedAtRoute(GetGameByIdRoute, new { id = game.Id }, new GameDetailsDto(
    game.Id,
    game.Title,
    game.Genre.Id,
    game.Price,
    game.ReleaseDate,
    game.Description
  ));
})
.WithParameterValidation();

app.MapPut("/games/{id:guid}", (Guid id, UpdateGameDto gameDto) =>
{
  var genre = allGenres.Find(genre => genre.Id == gameDto.GenreId);

  if (genre is null)
  {
    return Results.BadRequest("Invalid genre Id.");
  }

  var existingGame = allGames.Find(game => game.Id == id);

  if (existingGame is null)
  {
    return Results.NotFound();
  }

  existingGame.Title = gameDto.Title;
  existingGame.Genre = genre;
  existingGame.Price = gameDto.Price;
  existingGame.ReleaseDate = gameDto.ReleaseDate;
  existingGame.Description = gameDto.Description;

  return Results.NoContent();
})
.WithParameterValidation();

app.MapDelete("/games/{id:guid}", (Guid id) =>
{
  allGames.RemoveAll(game => game.Id == id);

  return Results.NoContent();
});

app.MapGet("/genres", () => allGenres.Select(genre => new GenreDto(
  genre.Id,
  genre.Name
)));

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
  [Required][StringLength(50, MinimumLength = 3)] string Title,
  Guid GenreId,
  [Range(0.01, 100.00)] decimal Price,
  DateOnly ReleaseDate,
  [Required][StringLength(500)] string Description
);

public record UpdateGameDto(
  [Required][StringLength(50, MinimumLength = 3)] string Title,
  Guid GenreId,
  [Range(0.01, 100.00)] decimal Price,
  DateOnly ReleaseDate,
  [Required][StringLength(500)] string Description
);

public record GenreDto(Guid Id, string Name);