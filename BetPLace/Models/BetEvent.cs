namespace BetPlace.Models
{
    public class BetEvent
    {
        public int Id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public decimal coef1 { get; set; }
        public decimal coef0 { get; set; }
        public decimal coef2 { get; set; }

        public string Team1Description { get; set; }
        public string Team2Description { get; set; }

        public bool IsActive { get; set; }

    }
}
