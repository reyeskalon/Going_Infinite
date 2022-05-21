using System.Data.SqlClient;
using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public class CardSqlDao : ICardDao
    {
        private readonly string connectionString = "Server=DESKTOP-JUOLNMK;Database=going_infinite;Trusted_Connection=True;";

        public Card GetCardById(string cardId)
        {
            Card card = new Card();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM cards WHERE card_id = @card_id", con);
                    cmd.Parameters.AddWithValue("@card_id", cardId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        card = CreateCardFromReader(reader);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return card;
        }
        public Card CreateCardFromReader(SqlDataReader reader)
        {
            Card card = new Card()
            {
                CardId = Convert.ToString(reader["card_id"]),
                Name = Convert.ToString(reader["name"]),
                Rarity = Convert.ToString(reader["rarity"]),
                ManaCost = Convert.ToString(reader["mana_cost"]),
                Cmc = Convert.ToDouble(reader["cmc"]),
                TypeLine = Convert.ToString(reader["type_line"]),
                OracleText = Convert.ToString(reader["oracle_text"]),
                Colors = Convert.ToString(reader["colors"]).Split(','),
                ColorIdentity = Convert.ToString(reader["color_identity"]).Split(','),
                Keywords = Convert.ToString(reader["keywords"]).Split(','),
                Set = Convert.ToString(reader["set_code"]),
                SetName = Convert.ToString(reader["set_name"]),
                Image = Convert.ToString(reader["image"]),
                ArtCrop = Convert.ToString(reader["art_crop"])
            };

            return card;
        }
    }
}
