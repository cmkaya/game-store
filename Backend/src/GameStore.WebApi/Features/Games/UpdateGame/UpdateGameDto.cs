using System.ComponentModel.DataAnnotations;

namespace GameStore.WebApi.Features.Games.UpdateGame;

public record UpdateGameDto(
  [Required][StringLength(50, MinimumLength = 3)] string Title,
  Guid GenreId,
  [Range(0.01, 100.00)] decimal Price,
  DateOnly ReleaseDate,
  [Required][StringLength(500)] string Description
);