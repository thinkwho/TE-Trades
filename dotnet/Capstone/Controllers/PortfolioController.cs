using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PortfolioController : ControllerBase
    {

        private readonly IPortfolioDao portfolioDao;

        public PortfolioController(IPortfolioDao _portfolioDao)
        {
            portfolioDao = _portfolioDao;
        }

        [HttpGet("{username}/game/{gameId}/balance")]
        public decimal GetGameBalance(string username, int gameId)
        {
            return portfolioDao.GetGameBalance(username, gameId);
        }

        [HttpPost("/newportfolio/game/{gameId}")]
        public int AddNewPortfolio(List<int> userIds, int gameId)
        {
            return portfolioDao.AddNewPortfolio(userIds, gameId);
        }
        [HttpPut("/{userId}/{gameId}/update")]
        public int UpdateGameBalance(int gameId, decimal amount)
        {
            return portfolioDao.UpdateBalance(gameId, amount);
        }

        [HttpGet("{username}/{gameId}")]
        public int GetPortfolioId(int gameId, string username)
        {
            return portfolioDao.GetPortfolioId(gameId, username);
        }

        [HttpGet("{portfolioId}")]
        public List<ActiveStocks> GetActiveStocks(int portfolioId)
        {
            return portfolioDao.GetAllActiveStocks(portfolioId);
        }

    }
}
