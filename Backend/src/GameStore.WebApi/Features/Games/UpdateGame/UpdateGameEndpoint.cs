using GameStore.WebApi.Data;

namespace GameStore.WebApi.Features.Games.UpdateGames;

public static class UpdateGameEndpoint
{
  public static void MapUpdateGame(this IEndpointRouteBuilder app, GameStoreData data)
  {
    app.MapPut("/{id:guid}", (Guid id, UpdateGameDto gameDto) =>
    {
      var existingGame = data.GetGame(id);
      if (existingGame is null)
      {
        return Results.NotFound();
      }

      var genre = data.GetGenre(existingGame.Genre.Id);
      if (genre is null)
      {
        return Results.BadRequest("Invalid genre Id.");
      }

      existingGame.Title = gameDto.Title;
      existingGame.Genre = genre;
      existingGame.Price = gameDto.Price;
      existingGame.ReleaseDate = gameDto.ReleaseDate;
      existingGame.Description = gameDto.Description;

      return Results.NoContent();
    }).WithParameterValidation();
  }
}