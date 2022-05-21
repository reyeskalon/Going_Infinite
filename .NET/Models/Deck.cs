namespace GoingInfinite.Models
{
    public class Deck
    {
        public int DeckId { get; set; }
        public int PlayerId { get; set; }
        public List<Card> Cards { get; set; }
    }
}
