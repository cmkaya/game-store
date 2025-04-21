using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games.Constants;
using GameStore.WebApi.Models;

namespace GameStore.WebApi.Features.Games.PostGame;

public static class PostGameEndpoint
{
  public static void MapCreateGame(this IEndpointRouteBuilder app)
  {
    app.MapPost("/", (CreateGameDto gameDto, GameStoreData data, GameDataLogger logger) =>
    {
      var genre = data.GetGenre(gameDto.GenreId);
      if (genre is null)
      {
        return Results.BadRequest("Invalid genre Id.");
      }

      var game = new Game()
      {
        Title = gameDto.Title,
        GenreId = gameDto.GenreId,
        Genre = genre,
        Price = gameDto.Price,
        ReleaseDate = gameDto.ReleaseDate,
        Description = gameDto.Description
      };

      data.AddGame(game);

      logger.PrintGames();

      return Results.CreatedAtRoute(EndpointNames.GetGame, new { id = game.Id }, new CreateGameResponseDto(
        game.Id,
        game.Title,
        game.Genre.Id,
        game.Price,
        game.ReleaseDate,
        game.Description
      ));
    }).WithParameterValidation();
  }
}