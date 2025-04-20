using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games.Constants;

namespace GameStore.WebApi.Features.Games.GetGame;

public static class GetGameEndpoint
{
  public static void MapGetGame(this IEndpointRouteBuilder app)
  {
    app.MapGet("/{id:guid}", (Guid id, GameStoreData data) =>
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
    }).WithName(EndpointNames.GetGame);
  }
}