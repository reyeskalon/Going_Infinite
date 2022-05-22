namespace GoingInfinite.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int DeckId { get; set; }
        public int EventId { get; set; }
        public string Opponent { get; set; }
        public string Outcome { get; set; }
    }
}
