using GameStore.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.WebApi.Data;

public static class DataExtensions
{
  public static void InitializeDatabase(this WebApplication app)
  {
    app.MigrateDatabase();
    app.Seed();
  }
  
  private static void MigrateDatabase(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    dbContext.Database.Migrate();
  }

  private static void Seed(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

    if (!dbContext.Genres.Any())
    {
      dbContext.Genres.AddRange(
        new Genre { Name = "Action" },
        new Genre { Name = "Adventure" },
        new Genre { Name = "RPG" },
        new Genre { Name = "Simulation" },
        new Genre { Name = "Strategy" }
      );
      dbContext.SaveChanges();
    }
  }
}