using Microsoft.Identity.Client;

namespace BetPlace.Models
{
    public class BalanceLog
    {
        public int Id { get; set; }
        public decimal Change { get; set; }
        public int UserId { get; set; }

        public int OperationType { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
