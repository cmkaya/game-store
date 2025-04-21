using GameStore.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.WebApi.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
  public DbSet<Game> Games => Set<Game>();
  public DbSet<Genre> Genres => Set<Genre>();
}