namespace GameStore.WebApi.Models;

public class Game
{
  public Guid Id { get; set; }

  public required string Title { get; set; }
  
  public Guid GenreId { get; set; }

  public Genre? Genre { get; set; }

  public decimal Price { get; set; }

  public DateOnly ReleaseDate { get; set; }

  public required string Description { get; set; }
}