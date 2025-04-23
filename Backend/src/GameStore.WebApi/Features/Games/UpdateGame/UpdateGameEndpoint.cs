using GameStore.WebApi.Data;

namespace GameStore.WebApi.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
  public static void MapUpdateGame(this IEndpointRouteBuilder app)
  {
    app.MapPut("/{id:guid}", (Guid id, UpdateGameDto gameDto, GameStoreContext db) =>
    {
      var existingGame = db.Games.Find(id);
      if (existingGame is null)
      {
        return Results.NotFound();
      }

      existingGame.Title = gameDto.Title;
      existingGame.GenreId = gameDto.GenreId;
      existingGame.Price = gameDto.Price;
      existingGame.ReleaseDate = gameDto.ReleaseDate;
      existingGame.Description = gameDto.Description;
      
      db.SaveChanges();

      return Results.NoContent();
    }).WithParameterValidation();
  }
}