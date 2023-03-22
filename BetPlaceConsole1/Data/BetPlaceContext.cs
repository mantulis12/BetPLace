using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BetPlace.Models;

namespace BetPlaceConsole1.Data
{
    public class BetPlaceContext : DbContext
    {
        public DbSet<BetPlace.Models.BetEvent> BetEvent { get; set; } = default!;

        public DbSet<BetPlace.Models.EventResult> EventResult { get; set; } = default!;

        public DbSet<BetPlace.Models.User> User { get; set; } = default!;

        public DbSet<BetPlace.Models.Bet> Bet { get; set; } = default!;

        public DbSet<BetPlace.Models.Bet> BalanceLog { get; set; } = default!;

        public DbSet<BetPlace.Models.BalanceLog> BalanceLog_1 { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=BetEvents;User Id=SA;Password=Secret1234;Encrypt=false");
        }
    }
}
