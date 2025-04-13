using GameStore.WebApi.Models;

namespace GameStore.WebApi.Data;

public class GameStoreData
{
  private readonly List<Genre> _allGenres =
  [
    new Genre { Id = Guid.NewGuid(), Name = "Action-adventure" },
    new Genre { Id = Guid.NewGuid(), Name = "Platformer" },
    new Genre { Id = Guid.NewGuid(), Name = "Action RPG" },
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
        Genre = _allGenres[0],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2017, 3, 3),
        Description = "An open-world action-adventure game set in the vast kingdom of Hyrule."
      },
      new Game
      {
        Id = Guid.NewGuid(),
        Title = "Super Mario Odyssey",
        Genre = _allGenres[1],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2017, 10, 27),
        Description = "A 3D platformer where Mario travels across various kingdoms to rescue Princess Peach."
      },
      new Game
      {
        Id = Guid.NewGuid(),
        Title = "The Witcher 3: Wild Hunt",
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