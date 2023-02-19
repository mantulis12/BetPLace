using BetPlace.Data;
using BetPlace.Models;
using Microsoft.AspNetCore.Connections.Features;
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

        public void MakeBet(int EventId, int UserId, decimal amount, string WinningTeam)
        {
            // Checking if balance is good
            User? user = _context.User
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

            BalanceLog balanceLog = new BalanceLog
            {
                Change = amount,
                CurrentBalance = balance,
                UserId = user.Id
            };

            _context.Add(balanceLog);

            BetEvent? betEvent = _context.BetEvent.Where(m => m.Id == EventId).FirstOrDefault();

            if (betEvent == null) {
                throw new Exception("Bet event not found");
            }

            var coef = betEvent.coef0;

            if (betEvent.Team1 == WinningTeam)
            {
                coef = betEvent.coef1;
            } else if (betEvent.Team2 == WinningTeam) {
                coef = betEvent.coef2;
            }

            // adding bet
            Bet bet = new Bet
            {
                balance = amount,
                BetEventId = EventId,
                UserId = UserId,
                WiningTeam = WinningTeam,
                coef = coef
            };

            _context.Add(bet);
                
        }
        
    }
}
