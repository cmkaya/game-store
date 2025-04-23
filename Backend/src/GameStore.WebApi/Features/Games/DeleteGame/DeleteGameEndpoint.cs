using GameStore.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.WebApi.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
  public static void MapDeleteGame(this IEndpointRouteBuilder app)
  {
    app.MapDelete("/{id:guid}", (Guid id, GameStoreContext db) =>
    {
      db.Games
        .Where(game => game.Id == id)
        .ExecuteDelete();

      return Results.NoContent();
    });
  }
}