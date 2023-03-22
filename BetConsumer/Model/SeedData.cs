using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BetPlace.Data;
using System;
using System.Linq;

namespace BetPlace.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BetPlaceContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<BetPlaceContext>>()))
        {
            // Look for any movies.
            if (context.BetEvent.Any())
            {
                return;   // DB has been seeded
            }
            context.BetEvent.AddRange(
                new BetEvent
                {
                    Team1 = "Manchester United",
                    Team2 = "Zalgiris",
                    EventStartDate = DateTime.Parse("2023-06-05 15:00"),
                    EventEndDate = DateTime.Parse("2023-06-10 17:00"),
                    coef0 = 3.00M,
                    coef1 = 1.01M,
                    coef2 = 5.00M,
                    Team1Description = "Team from England",
                    Team2Description = "Team From Lithuania",
                    IsActive = false,
                }, 
                new BetEvent
                {
                    Team1 = "Zalgiris",
                    Team2 = "Manchester United",
                    EventStartDate = DateTime.Parse("2023-02-01 15:00"),
                    EventEndDate = DateTime.Parse("2023-02-15 15:00"),
                    coef0 = 3.00M,
                    coef2 = 1.01M,
                    coef1 = 5.00M,
                    Team1Description = "Team from England",
                    Team2Description = "Team From Lithuania",
                    IsActive = true,
                }
            );
            context.SaveChanges();
        }
    }
}
