using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public interface IMatchDao
    {
        public Match GetMatchById(int matchId);
        public List<Match> GetMatchesByEvent(int eventId);
        public List<Match> GetMatchesByPlayer(int playerId);
        public Match AddNewMatch(Match match);
    }
}
