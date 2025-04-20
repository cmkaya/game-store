using GameStore.WebApi.Features.Games.DeleteGame;
using GameStore.WebApi.Features.Games.GetGame;
using GameStore.WebApi.Features.Games.GetGames;
using GameStore.WebApi.Features.Games.PostGame;
using GameStore.WebApi.Features.Games.UpdateGame;

namespace GameStore.WebApi.Features.Games;

public static class GamesEndpoints
{
  public static void MapGames(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/games");

    group.MapGetGames();
    group.MapGetGame();
    group.MapUpdateGame();
    group.MapCreateGame();
    group.MapDeleteGame();
  }
}