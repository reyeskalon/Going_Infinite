using System.Data.SqlClient;
using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public class FNMSqlDao : IFNMDao
    {
        public FNMEvent GetEventById(int eventId)
        {
            return new FNMEvent();
        }
        public List<FNMEvent> GetEventsByPlayer(int playerId)
        {
            return new List<FNMEvent>();
        }
        public FNMEvent AddNewFNMEvent()
        {
            return new FNMEvent();
        }
    }
}
