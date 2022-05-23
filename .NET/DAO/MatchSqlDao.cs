using System.Data.SqlClient;
using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public class MatchSqlDao : IMatchDao
    {
        private readonly string connectionString = "Server=DESKTOP-JUOLNMK;Database=going_infinite;Trusted_Connection=True;";
        public Match GetMatchById(int matchId)
        {
            Match match = null;

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM matches WHERE match_id = @match_id", conn);
                    cmd.Parameters.AddWithValue("@match_id", matchId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        match = CreateMatchFromReader(reader);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return match;
        }
        public List<Match> GetMatchesByEvent(int eventId)
        {
            List<Match> matches = new List<Match>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM matches WHERE event_id = @event_id", conn);
                    cmd.Parameters.AddWithValue("@event_id", eventId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        matches.Add(CreateMatchFromReader(reader));
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return matches;
        }
        public List<Match> GetMatchesByPlayer(int playerId)
        {
            List<Match> matches = new List<Match>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM matches WHERE player_id = @player_id", conn);
                    cmd.Parameters.AddWithValue("@player_id", playerId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        matches.Add(CreateMatchFromReader(reader));
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return matches;
        }
        public Match AddNewMatch(Match match)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO matches (player_id, deck_id, event_id, opponent, outcome) OUTPUT INSERTED.match_id VALUES (@player_id, @deck_id, @event_id, @opponent, @outcome)", conn);
                    cmd.Parameters.AddWithValue("@player_id", match.PlayerId);
                    cmd.Parameters.AddWithValue("@deck_id", match.DeckId);
                    cmd.Parameters.AddWithValue("@event_id", match.EventId);
                    cmd.Parameters.AddWithValue("@opponent", match.Opponent);
                    cmd.Parameters.AddWithValue("@outcome", match.Outcome);

                    match.PlayerId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return match;
        }

        public Match CreateMatchFromReader(SqlDataReader reader)
        {
            Match match = new Match()
            {
                MatchId = Convert.ToInt32(reader["match_id"]),
                PlayerId = Convert.ToInt32(reader["player_id"]),
                DeckId = Convert.ToInt32(reader["deck_id"]),
                EventId = Convert.ToInt32(reader["event_id"]),
                Opponent = Convert.ToString(reader["opponent"]),
                Outcome = Convert.ToString(reader["outcome"])
            };

            return match;
        }
    }
}
