using GameStore.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameByIdRoute = "GetGameById";

List<Game> allGames =
[
  new Game
  {
    Id = Guid.NewGuid(),
    Title = "The Legend of Zelda: Breath of the Wild",
    Genre = "Action-adventure",
    Price = 59.99m,
    ReleaseDate = new DateOnly(2017, 3, 3)
  },
  new Game
  {
    Id = Guid.NewGuid(),
    Title = "Super Mario Odyssey",
    Genre = "Platformer",
    Price = 59.99m,
    ReleaseDate = new DateOnly(2017, 10, 27)
  },
  new Game
  {
    Id = Guid.NewGuid(),
    Title = "The Witcher 3: Wild Hunt",
    Genre = "Action RPG",
    Price = 39.99m,
    ReleaseDate = new DateOnly(2015, 5, 19)
  },
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

app.Run();