using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games.Constants;
using GameStore.WebApi.Models;

namespace GameStore.WebApi.Features.Games.PostGame;

public static class PostGameEndpoint
{
  public static void MapCreateGame(this IEndpointRouteBuilder app)
  {
    app.MapPost("/", (CreateGameDto gameDto, GameStoreContext db) =>
    {
      var game = new Game()
      {
        Title = gameDto.Title,
        GenreId = gameDto.GenreId,
        Price = gameDto.Price,
        ReleaseDate = gameDto.ReleaseDate,
        Description = gameDto.Description
      };

      db.Games.Add(game);
      db.SaveChanges();

      return Results.CreatedAtRoute(EndpointNames.GetGame, new { id = game.Id }, new CreateGameResponseDto(
        game.Id,
        game.Title,
        game.GenreId,
        game.Price,
        game.ReleaseDate,
        game.Description
      ));
    }).WithParameterValidation();
  }
}