using BetPlace.Data;
using BetPlace.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BetPlace.Services
{
    public class BetService
    {
        private BetPlaceContext _context;
        public BetService(BetPlaceContext context)
        {
            _context = context;
        }

        public void MakeBet(int EventId, int UserId, double amount, string WinningTeam)
        {
            // Checking if balance is good
            var user = _context.User
                .FirstOrDefault(m => m.Id == UserId);

            if (user == null)
            {
                throw new Exception("User Not Found");
            }

            var balance = user.Balance;

            if (balance < amount)
            {
                throw new Exception("Balance is not enough");
            }
            // taking balance

            user.Balance = user.Balance - amount;
            _context.Update(user);
            // adding bet
            var bet = new Bet
            {
                balance = amount,
                BetEventId = EventId,
                UserId = UserId,
                WiningTeam = WinningTeam
            };

            _context.Add(bet);
                
        }
        
    }
}
