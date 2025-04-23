using GameStore.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.WebApi.Features.Games.GetGames;

public static class GetGamesEndpoint
{
  public static void MapGetGames(this IEndpointRouteBuilder app)
  {
    app.MapGet("/", (GameStoreContext db) =>
      db.Games
        .Include(game => game.Genre)
        .Select(game => new GameSummaryDto(
          game.Id,
          game.Title,
          game.Genre!.Name,
          game.Price,
          game.ReleaseDate
        ))
        .AsNoTracking()
    );
  }
}