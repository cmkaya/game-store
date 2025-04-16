using GameStore.WebApi.Data;

namespace GameStore.WebApi.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
  public static void MapGetGenres(this IEndpointRouteBuilder app, GameStoreData data)
  {
    app.MapGet("/", () =>
      data.GetGenres().Select(genre => new GenreDto(genre.Id, genre.Name))
    );
  }
}