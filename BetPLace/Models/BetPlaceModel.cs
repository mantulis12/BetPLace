namespace BetPlace.Models
{
    public class BetPlaceModel
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public decimal amount { get; set; }
        public string WinningTeam { get; set; }
    }
}
