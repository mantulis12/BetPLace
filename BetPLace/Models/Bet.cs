namespace BetPlace.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public string WiningTeam { get; set; }
        public int BetEventId { get; set; }

        public double balance { get; set; }
        public BetEvent BetEvent { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
