using GameStore.WebApi.Data;

namespace GameStore.WebApi.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
  public static void MapGetGenres(this IEndpointRouteBuilder app)
  {
    app.MapGet("/", (GameStoreData data) =>
    {
      var genres = data.GetGenres();

      return Results.Ok(genres.Select(genre => new GenreDto(genre.Id, genre.Name)));
    });
  }
}