using GameStore.WebApi.Models;

namespace GameStore.WebApi.Data;

public class GameStoreData
{
  private readonly List<Genre> _allGenres =
  [
    new() { Id = new Guid("c8d4c053-3d73-4d83-8d6a-c9b0348a7d6c"), Name = "Action-adventure" },
    new() { Id = new Guid("15726e7b-6c2b-4d84-a779-9e53467a8ea9"), Name = "Platformer" },
    new() { Id = new Guid("8c7279ae-bcd8-4c27-86c9-47b9529b2f8b"), Name = "Action RPG" },
  ];

  private readonly List<Game> _allGames;

  public GameStoreData()
  {
    _allGames =
    [
      new Game
      {
        Id = Guid.NewGuid(),
        Title = "The Legend of Zelda: Breath of the Wild",
        GenreId = _allGenres[0].Id,
        Genre = _allGenres[0],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2017, 3, 3),
        Description = "An open-world action-adventure game set in the vast kingdom of Hyrule."
      },
      new Game
      {
        Id = Guid.NewGuid(),
        Title = "Super Mario Odyssey",
        GenreId = _allGenres[1].Id,
        Genre = _allGenres[1],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2017, 10, 27),
        Description = "A 3D platformer where Mario travels across various kingdoms to rescue Princess Peach."
      },
      new Game
      {
        Id = Guid.NewGuid(),
        Title = "The Witcher 3: Wild Hunt",
        GenreId = _allGenres[2].Id,
        Genre = _allGenres[2],
        Price = 39.99m,
        ReleaseDate = new DateOnly(2015, 5, 19),
        Description = "An open-world RPG where players control Geralt of Rivia, a monster hunter, in a richly detailed fantasy world."
      }
    ];
  }

  public IEnumerable<Game> GetGames() => _allGames;

  public Game? GetGame(Guid id) => _allGames.Find(game => game.Id == id);

  public void AddGame(Game game)
  {
    game.Id = Guid.NewGuid();
    _allGames.Add(game);
  }

  public void RemoveGame(Guid id)
  {
    _allGames.RemoveAll(game => game.Id == id);
  }

  public IEnumerable<Genre> GetGenres() => _allGenres;

  public Genre? GetGenre(Guid id) => _allGenres.Find(genre => genre.Id == id);
}