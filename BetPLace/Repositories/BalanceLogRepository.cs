using BetPlace.Data;
using BetPlace.Models;

namespace BetPlace.Repositories
{
    public class BalanceLogRepository
    {
        private BetPlaceContext _context;
        public BalanceLogRepository(BetPlaceContext context)
        {
            _context = context;
        }

        public void AddBalanceLog(decimal Change, decimal Balance, int UserId, int OperationType)
        {
            BalanceLog balanceLog = new BalanceLog
            {
                Change = Change,
                CurrentBalance = Balance,
                UserId = UserId,
                OperationType = OperationType
            };

            _context.Add(balanceLog);
        }
    }
}
