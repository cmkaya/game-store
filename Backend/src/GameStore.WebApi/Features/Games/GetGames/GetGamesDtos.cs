namespace GameStore.WebApi.Features.Games.GetGames;

public record GameSummaryDto(
  Guid Id,
  string Title,
  string Genre,
  decimal Price,
  DateOnly ReleaseDate
);