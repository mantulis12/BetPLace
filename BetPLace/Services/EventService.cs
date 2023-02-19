using BetPlace.Data;
using BetPlace.Models;

namespace BetPlace.Services
{
    public class EventService
    {

        private BetPlaceContext _context;
        public EventService(BetPlaceContext context)
        {
            _context = context;
        }

        public void ResultEvent(int EventId, EventResult eventResult)
        {
            List < Bet > bets = _context.Bet.Where(m => m.BetEventId == EventId).ToList();

            

            foreach (var bet in bets)
            {
                User? user = _context.User
                .FirstOrDefault(m => m.Id == bet.UserId);

                if (eventResult.WiningTeam == bet.WiningTeam)
                {
                    
                    bet.status = 1;
                    _context.Update(bet);
                    if (user == null) {
                        continue;
                    }

                    decimal amount = Decimal.Multiply(bet.balance, bet.coef);


                    BalanceLog balanceLog = new BalanceLog
                    {
                        UserId = bet.UserId,
                        Change = amount,
                        CurrentBalance = user.Balance,
                    };

                    _context.Add(balanceLog);

                    user.Balance += amount;

                    _context.Update(user);
                } else {
                    bet.status = 1;
                    _context.Update(bet);
                }
            }

        }
    }
}
