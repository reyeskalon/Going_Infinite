using Microsoft.AspNetCore.Mvc;
using GoingInfinite.DAO;
using GoingInfinite.Models;
namespace GoingInfinite.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FNMController : ControllerBase
    {
        private readonly IFNMDao FNMDao = new FNMSqlDao();

        [HttpGet("{eventId}")]
        public FNMEvent GetEventById(int eventId)
        {
            return FNMDao.GetEventById(eventId);
        }
        [HttpGet("/FNM/Player/{playerId}")]
        public List<FNMEvent> GetFNMEventsByPlayer(int playerId)
        {
            return FNMDao.GetEventsByPlayer(playerId);
        }
        [HttpPost]
        public FNMEvent AddNewFNMEvent()
        {
            return FNMDao.AddNewFNMEvent();
        }
    }
}
