using System.ComponentModel.DataAnnotations;

namespace GameStore.WebApi.Features.Games.PostGame;

public record CreateGameDto(
  [Required][StringLength(50, MinimumLength = 3)] string Title,
  Guid GenreId,
  [Range(0.01, 100.00)] decimal Price,
  DateOnly ReleaseDate,
  [Required][StringLength(500)] string Description
);

public record CreateGameResponseDto(
  Guid Id,
  string Title,
  Guid GenreId,
  decimal Price,
  DateOnly ReleaseDate,
  string Description
);