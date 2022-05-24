using System.Data.SqlClient;
using GoingInfinite.Models;

namespace GoingInfinite.DAO
{
    public class DeckSqlDao : IDeckDao
    {
        private readonly string connectionString = "Server=DESKTOP-JUOLNMK;Database=going_infinite;Trusted_Connection=True;";
        private readonly ICardDao cardDao = new CardSqlDao();
        public Deck GetDeckById(int deckId)
        {
            Deck deck = null;
            int playerId = 0;
            List<Card> cards = new List<Card>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT player_id FROM decks WHERE deck_id = @deck_id", con);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);
                    playerId = Convert.ToInt32(cmd.ExecuteScalar());

                    con.Close();
                    con.Open();

                    cmd = new SqlCommand("SELECT * FROM cards_in_deck WHERE deck_id = @deck_id", con);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int count = Convert.ToInt32(reader["number_of_cards"]);
                        Card card = cardDao.GetCardById(Convert.ToString(reader["card_id"]));
                        card.Count = count;
                        cards.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            deck = new Deck()
            {
                DeckId = deckId,
                PlayerId = playerId,
                Cards = cards
            };
            return deck;
        }
        public Deck GetDeckByEventAndPlayer(int eventId, int playerId)
        {
            Deck deck = null;
            int deckId = 0;
            List<Card> cards = new List<Card>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT deck_id FROM matches WHERE event_id = @event_id AND player_id = @player_id;", con);
                    cmd.Parameters.AddWithValue("@event_id", eventId);
                    cmd.Parameters.AddWithValue("@player_id", playerId);

                    deckId = Convert.ToInt32(cmd.ExecuteScalar());

                    if(deckId != 0)
                    {
                        deck = GetDeckById(deckId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return deck;
        }
        public List<Deck> GetDecksByPlayer(int playerId)
        {
            List<Deck> decks = new List<Deck>();
            List<int> deckIds = new List<int>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE player_id = @player_id", con);
                    cmd.Parameters.AddWithValue("@player_id", playerId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        deckIds.Add(Convert.ToInt32(reader["deck_id"]));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach(int id in deckIds)
            {
                decks.Add(GetDeckById(id));
            }
            return decks;
        }
        public Deck AddNewDeck(Deck deck)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO decks(player_id) OUTPUT INSERTED.deck_id VALUES(@player_id);", con);
                    cmd.Parameters.AddWithValue("@player_id", deck.PlayerId);
                    deck.DeckId = Convert.ToInt32(cmd.ExecuteScalar());

                    con.Close();
                    con.Open();

                    foreach(Card card in deck.Cards)
                    {
                        cmd = new SqlCommand("INSERT INTO cards_in_deck(deck_id, card_id, number_of_cards) VALUES(@deck_id, @card_id, @number_of_cards);", con);
                        cmd.Parameters.AddWithValue("@deck_id", deck.DeckId);
                        cmd.Parameters.AddWithValue("@card_id", card.CardId);
                        cmd.Parameters.AddWithValue("@number_of_cards", card.Count);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return deck;
        }
    }
}
