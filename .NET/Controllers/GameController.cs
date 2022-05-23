using Microsoft.AspNetCore.Mvc;
using GoingInfinite.DAO;
using GoingInfinite.Models;

namespace GoingInfinite.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameDao GameDao = new GameSqlDao();

        [HttpGet("{gameId}")]
        public Game GetGameById(int gameId)
        {
            return GameDao.GetGameById(gameId);
        }
        [HttpGet("Match/{matchId}")]
        public List<Game> GetGamesInMatch(int matchId)
        {
            return GameDao.GetGamesInMatch(matchId);
        }
        [HttpGet("Player/{playerId}")]
        public List<Game> GetGamesByPlayer(int playerId)
        {
            return GameDao.GetGamesByPlayer(playerId);
        }
        [HttpPost]
        public Game AddNewGame(Game game)
        {
            return GameDao.AddNewGame(game);
        }
    }
}
