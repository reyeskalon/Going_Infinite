using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public class MatchSqlDao : IMatchDao
    {
        public Match GetMatchById(int matchId)
        {
            return new Match();
        }
        public List<Match> GetMatchesByEvent(int eventId)
        {
            return new List<Match>();
        }
        public List<Match> GetMatchesByPlayer(int playerId)
        {
            return new List<Match>();
        }
        public Match AddNewMatch(Match match)
        {
            return match;
        }
    }
}
