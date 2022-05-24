using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public interface IGameDao
    {
        public Game GetGameById(int gameId);
        public List<Game> GetGamesInMatch(int matchId);
        public List<Game> GetGamesByPlayer(int playerId);
        public List<Game> GetGamesByEvent(int eventId);
        public Game AddNewGame(Game game);
    }
}
