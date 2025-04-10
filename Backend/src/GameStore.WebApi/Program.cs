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

app.MapGet("/games", () => allGames);

app.MapGet("/games/{id:guid}", (Guid id) =>
{
  var game = allGames.Find(g => g.Id == id);

  return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameByIdRoute);

app.MapPost("/games", (Game game) =>
{
  game.Id = Guid.NewGuid();
  allGames.Add(game);

  return Results.CreatedAtRoute(GetGameByIdRoute, new { id = game.Id }, game);
})
.WithParameterValidation();

app.MapPut("/games/{id:guid}", (Guid id, Game updatedGame) =>
{
  var existingGame = allGames.Find(game => game.Id == id);

  if (existingGame is null)
  {
    return Results.NotFound();
  }

  existingGame.Title = updatedGame.Title;
  existingGame.Genre = updatedGame.Genre;
  existingGame.Price = updatedGame.Price;
  existingGame.ReleaseDate = updatedGame.ReleaseDate;

  return Results.NoContent();
})
.WithParameterValidation();

app.MapDelete("/games/{id:guid}", (Guid id) =>
{
  allGames.RemoveAll(game => game.Id == id);

  return Results.NoContent();
});

app.Run();