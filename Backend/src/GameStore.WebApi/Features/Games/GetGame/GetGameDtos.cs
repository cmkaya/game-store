namespace GameStore.WebApi.Features.Games.GetGame;

public record GameDetailsDto(
  Guid Id,
  string Title,
  Guid GenreId,
  decimal Price,
  DateOnly ReleaseDate,
  string Description
);