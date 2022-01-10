using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IPortfolioDao
    {
        public decimal GetGameBalance(string username, int gameId);

        public int AddNewPortfolio(List<int> userIds, int gameId);

        public int UpdateBalance(int gameId, decimal amount);

        public int GetPortfolioId(int gameId, string username);

        public List<ActiveStocks> GetAllActiveStocks(int portfolioId);

    }
}
