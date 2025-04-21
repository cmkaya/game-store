using GameStore.WebApi.Data;

namespace GameStore.WebApi.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
  public static void MapUpdateGame(this IEndpointRouteBuilder app)
  {
    app.MapPut("/{id:guid}", (Guid id, UpdateGameDto gameDto, GameStoreData data) =>
    {
      var existingGame = data.GetGame(id);
      if (existingGame is null)
      {
        return Results.NotFound();
      }

      var genre = data.GetGenre(existingGame.GenreId);
      if (genre is null)
      {
        return Results.BadRequest("Invalid genre Id.");
      }

      existingGame.Title = gameDto.Title;
      existingGame.GenreId = gameDto.GenreId;
      existingGame.Genre = genre;
      existingGame.Price = gameDto.Price;
      existingGame.ReleaseDate = gameDto.ReleaseDate;
      existingGame.Description = gameDto.Description;

      return Results.NoContent();
    }).WithParameterValidation();
  }
}