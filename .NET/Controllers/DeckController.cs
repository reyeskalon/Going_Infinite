using Microsoft.AspNetCore.Mvc;
using GoingInfinite.DAO;
using GoingInfinite.Models;
namespace GoingInfinite.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckDao DeckDao = new DeckSqlDao();
        public DeckController()
        {
            DeckDao = new DeckSqlDao();
        }

        [HttpGet("{deckId}")]
        public Deck GetDeckById(int deckId)
        {
            return DeckDao.GetDeckById(deckId);
        }
        [HttpGet("Event/{eventId}/{playerId}")]
        public Deck GetDeckByEvent(int eventId, int playerId)
        {
            return DeckDao.GetDeckByEventAndPlayer(eventId, playerId);
        }
        [HttpGet("Player/{playerId}")]
        public List<Deck> GetDecksByPlayer(int playerId)
        {
            return DeckDao.GetDecksByPlayer(playerId);
        }
        [HttpPost]
        public Deck AddNewDeck(Deck deck)
        {
            return DeckDao.AddNewDeck(deck);
        }
    }
}
