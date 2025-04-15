using GameStore.WebApi.Data;

namespace GameStore.WebApi.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
  public static void MapDeleteGame(this IEndpointRouteBuilder app, GameStoreData data)
  {
    app.MapDelete("/{id:guid}", (Guid id) =>
    {
      data.RemoveGame(id);

      return Results.NoContent();
    });
  }
}