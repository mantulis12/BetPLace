namespace BetPlace.Models
{
    public class EventResult
    {
        public int Id { get; set; }
        public string WiningTeam { get; set; }

        public int BetEventId { get; set; }
        public BetEvent BetEvent { get; set; }
    }
}
