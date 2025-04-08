using System.ComponentModel.DataAnnotations;

namespace GameStore.WebApi.Models;

public class Game
{
  public Guid Id { get; set; }

  [Required]
  [StringLength(50, MinimumLength = 3)]
  public required string Title { get; set; }

  [Required]
  [StringLength(20, MinimumLength = 3)]
  public required string Genre { get; set; }

  [Range(0.01, 100.00)]
  public decimal Price { get; set; }

  public DateOnly ReleaseDate { get; set; }
}