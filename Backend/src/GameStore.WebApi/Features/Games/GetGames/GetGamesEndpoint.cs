using GameStore.WebApi.Data;

namespace GameStore.WebApi.Features.Games.GetGames;

public static class GetGamesEndpoint
{
  public static void MapGetGames(this IEndpointRouteBuilder app, GameStoreData data)
  {
    app.MapGet("/games", () =>
      data.GetGames().Select(game => new GameSummaryDto(
        game.Id,
        game.Title,
        game.Genre.Name,
        game.Price,
        game.ReleaseDate
      ))
    );
  }
}