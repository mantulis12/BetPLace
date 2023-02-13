using BetPlace.Data;
using Microsoft.AspNetCore.SignalR;

namespace BetPlace.Services
{
    public class BalanceService
    {
        private BetPlaceContext _context;
        public BalanceService(BetPlaceContext context) 
        {
            _context = context;
        }

        public void Substract(int UserId, double balance, double amount)
        {

        }
    }
}
