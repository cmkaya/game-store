namespace GameStore.WebApi.Models;

public class Game
{
  public Guid Id { get; set; }

  public required string Title { get; set; }

  public required string Genre { get; set; }

  public decimal Price { get; set; }

  public DateOnly ReleaseDate { get; set; }
}
