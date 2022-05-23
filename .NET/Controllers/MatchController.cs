using Microsoft.AspNetCore.Mvc;
using GoingInfinite.DAO;
using GoingInfinite.Models;

namespace GoingInfinite.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchDao MatchDao = new MatchSqlDao();

        [HttpGet("{matchId}")]
        public Match GetMatchById(int matchId)
        {
            return MatchDao.GetMatchById(matchId);
        }
        [HttpGet("Event/{eventId}")]
        public List<Match> GetMatchesByEvent(int eventId)
        {
            return MatchDao.GetMatchesByEvent(eventId);
        }
        [HttpGet("Player/{playerId}")]
        public List<Match> GetMatchesByPlayer(int playerId)
        {
            return MatchDao.GetMatchesByPlayer(playerId);
        }
        [HttpPost]
        public Match AddNewMatch(Match match)
        {
            return MatchDao.AddNewMatch(match);
        }
    }
}
