using GoingInfinite.Models;
namespace GoingInfinite.DAO
{
    public interface IDeckDao
    {
        public Deck GetDeckById(int deckId);
        public List<Deck> GetDecksByPlayer(int playerId);
        public Deck AddNewDeck(Deck deck);
        public Deck GetDeckByEventAndPlayer(int eventId, int playerId);
    }
}
