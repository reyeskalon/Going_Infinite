using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public interface ICardDao
    {
        public Card GetCardById(string cardId);
    }
}
