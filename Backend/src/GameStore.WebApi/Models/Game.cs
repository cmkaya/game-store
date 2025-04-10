using System.ComponentModel.DataAnnotations;

namespace GameStore.WebApi.Models;

public class Game
{
  public Guid Id { get; set; }

  [Required]
  [StringLength(50, MinimumLength = 3)]
  public required string Title { get; set; }

  public required Genre Genre { get; set; }

  [Range(0.01, 100.00)]
  public decimal Price { get; set; }

  public DateOnly ReleaseDate { get; set; }

  public required string Description { get; set; }
}