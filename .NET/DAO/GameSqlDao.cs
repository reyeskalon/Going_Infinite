using System.Data.SqlClient;
using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public class GameSqlDao : IGameDao
    {
        private readonly string connectionString = "Server=DESKTOP-JUOLNMK;Database=going_infinite;Trusted_Connection=True;";
        public Game GetGameById(int gameId)
        {
            Game game = null;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    SqlCommand cmd = new SqlCommand("SELECT * FROM games WHERE game_id = @game_id", conn);
                    cmd.Parameters.AddWithValue("@game_id", gameId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        game = CreateGameFromReader(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return game;
        }
        public List<Game> GetGamesInMatch(int matchId)
        {
            List<Game> games = new List<Game>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT game_id, games.match_id, games.outcome FROM games " +
                                                    "JOIN matches ON games.match_id = matches.match_id " +
                                                    "WHERE matches.match_id = @match_id; ", conn);
                    cmd.Parameters.AddWithValue("@match_id", matchId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        games.Add(CreateGameFromReader(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return games;
        }
        public List<Game> GetGamesByPlayer(int playerId)
        {
            List<Game> games = new List<Game>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT game_id, games.match_id, games.outcome FROM games " +
                                                    "JOIN matches ON games.match_id = matches.match_id " +
                                                    "WHERE matches.player_id = @player_id; ", conn);
                    cmd.Parameters.AddWithValue("@player_id", playerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        games.Add(CreateGameFromReader(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return games;
        }
        public Game AddNewGame(Game game)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO games (match_id, outcome) OUTPUT INSERTED.game_id VALUES(@match_id, @outcome)", conn);

                    game.GameId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return game;
        }
        public Game CreateGameFromReader(SqlDataReader reader)
        {
            Game game = new Game()
            {
                GameId = Convert.ToInt32(reader["game_id"]),
                MatchId = Convert.ToInt32(reader["match_id"]),
                Outcome = Convert.ToString(reader["outcome"])
            };
            return game;
        }
    }
}
