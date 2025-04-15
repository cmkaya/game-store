using GameStore.WebApi.Data;
using GameStore.WebApi.Features.Games.DeleteGame;
using GameStore.WebApi.Features.Games.GetGame;
using GameStore.WebApi.Features.Games.GetGames;
using GameStore.WebApi.Features.Games.PostGame;
using GameStore.WebApi.Features.Games.UpdateGames;

namespace GameStore.WebApi.Features.Games;

public static class GamesEndpoints
{
  public static void MapGames(this IEndpointRouteBuilder app, GameStoreData data)
  {
    var group = app.MapGroup("/games");

    group.MapGetGames(data);
    group.MapGetGame(data);
    group.MapUpdateGame(data);
    group.MapCreateGame(data);
    group.MapDeleteGame(data);
  }
}