using GameStore.WebApi.Features.Genres.GetGenres;

namespace GameStore.WebApi.Features.Genres;

public static class GenresEndpoints
{
  public static void MapGenres(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/genres");

    group.MapGetGenres();
  }
}