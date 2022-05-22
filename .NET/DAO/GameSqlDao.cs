using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public class GameSqlDao : IGameDao
    {
        public Game GetGameById(int gameId)
        {
            return new Game();
        }
        public List<Game> GetGamesInMatch(int matchId)
        {
            return new List<Game>();
        }
        public List<Game> GetGamesByPlayer(int playerId)
        {
            return new List<Game>();
        }
        public Game AddNewGame(Game game)
        {
            return game;
        }
    }
}
