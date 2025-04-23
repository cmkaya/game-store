using GameStore.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.WebApi.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
  public static void MapGetGenres(this IEndpointRouteBuilder app)
  {
    app.MapGet("/", (GameStoreContext db) =>
      db.Genres
        .Select(genre => new GenreDto(genre.Id, genre.Name))
        .AsNoTracking()
    );
  }
}