using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetPlace.Models;

namespace BetPlace.Data
{
    public class BetPlaceContext : DbContext
    {
        public BetPlaceContext(DbContextOptions<BetPlaceContext> options)
            : base(options)
        {
        }

        public DbSet<BetPlace.Models.BetEvent> BetEvent { get; set; } = default!;

        public DbSet<BetPlace.Models.EventResult> EventResult { get; set; } = default!;

        public DbSet<BetPlace.Models.User> User { get; set; } = default!;

        public DbSet<BetPlace.Models.Bet> Bet { get; set; } = default!;

        public DbSet<BetPlace.Models.Bet> BalanceLog { get; set; } = default!;

        public DbSet<BetPlace.Models.BalanceLog> BalanceLog_1 { get; set; } = default!;
    }
}
