using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public interface IFNMDao
    {
        public FNMEvent GetEventById(int eventId);
        public List<FNMEvent> GetEventsByPlayer(int playerId);
        public FNMEvent AddNewFNMEvent(FNMEvent draftEvent);
    }
}
