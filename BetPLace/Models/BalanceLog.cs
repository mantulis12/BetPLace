using Microsoft.Identity.Client;

namespace BetPlace.Models
{
    public class BalanceLog
    {
        public int Id { get; set; }
        public double Change { get; set; }
        public int UserId { get; set; }
        public double CurrentBalance { get; set; }
    }
}
