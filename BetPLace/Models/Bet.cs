namespace BetPlace.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public string WiningTeam { get; set; }

        public decimal coef { get; set; }
        public int status { get; set; }
        public int BetEventId { get; set; }

        public decimal balance { get; set; }
        public BetEvent BetEvent { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
