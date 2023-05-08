using BetPlace.Data;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using NuGet.Versioning;

namespace BetPlace.Models
{
    public class BetRepository
    {
        public BetPlaceContext _context;
        public BetRepository(BetPlaceContext context)
        {
            _context = context;
        }

        public void AddBet(decimal Amount, int EventId, int UserId, string WinningTeam, decimal coef)
        {
            Bet bet = new Bet
            {
                balance = Amount,
                BetEventId = EventId,
                UserId = UserId,
                WiningTeam = WinningTeam,
                coef = coef
            };

            _context.Add(bet);
        }
    }
}
