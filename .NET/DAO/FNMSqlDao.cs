using System.Data.SqlClient;
using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public class FNMSqlDao : IFNMDao
    {
        private readonly string connectionString = "Server=DESKTOP-JUOLNMK;Database=going_infinite;Trusted_Connection=True;";
        public FNMEvent GetEventById(int eventId)
        {
            FNMEvent draftEvent = null;

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM fnm WHERE event_id = @event_id;", conn);
                    cmd.Parameters.AddWithValue("@event_id", eventId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        draftEvent = CreateEventFromReader(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return draftEvent;
        }
        public List<FNMEvent> GetEventsByPlayer(int playerId)
        {
            List<FNMEvent> draftEvents =  new List<FNMEvent>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT fnm.event_id, fnm.event_date, lgs FROM fnm " +
                                                    "JOIN matches ON fnm.event_id = matches.event_id " +
                                                    "WHERE player_id = @player_id; ", conn);
                    cmd.Parameters.AddWithValue("@player_id", playerId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FNMEvent draftEvent = CreateEventFromReader(reader);
                        draftEvents.Add(draftEvent);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return draftEvents;
        }
        public FNMEvent AddNewFNMEvent(FNMEvent draftEvent)
        {
            List<FNMEvent> draftEvents = new List<FNMEvent>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO fnm (event_date, lgs) OUTPUT INSERTED.event_id VALUES (@event_date, @lgs)", conn);
                    cmd.Parameters.AddWithValue("@event_date", draftEvent.EventDate);
                    cmd.Parameters.AddWithValue("@lgs", draftEvent.LGS);

                    draftEvent.EventId = Convert.ToInt32(cmd.ExecuteScalar());
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return draftEvent;
        }
        public FNMEvent CreateEventFromReader(SqlDataReader reader)
        {
            FNMEvent draftEvent = new FNMEvent()
            {
                EventId = Convert.ToInt32(reader["event_id"]),
                EventDate = Convert.ToDateTime(reader["event_date"]).Date,
                LGS = Convert.ToString(reader["lgs"])
            };

            return draftEvent;
        }
    }
}
