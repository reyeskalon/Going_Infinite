using Microsoft.AspNetCore.Mvc;
using GoingInfinite.DAO;
using GoingInfinite.Models;
namespace GoingInfinite.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardDao CardDao = new CardSqlDao();

        [HttpGet("{cardId}")]
        public Card GetCardById(string cardId)
        {
            return CardDao.GetCardById(cardId);
        }
    }
}
