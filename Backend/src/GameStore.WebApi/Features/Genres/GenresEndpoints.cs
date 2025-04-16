using GameStore.WebApi.Data;

namespace GameStore.WebApi.Features.Genres.GetGenres;

public static class GenresEndpoints
{
  public static void MapGenres(this IEndpointRouteBuilder app, GameStoreData data)
  {
    var group = app.MapGroup("/genres");

    group.MapGetGenres(data);
  }
}