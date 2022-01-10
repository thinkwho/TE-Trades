using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{        // TEST for total sum portfolio (all stockes owned), not final 
    public class Portfolio
    {

        public int Portfolio_Id { get; set; }
        public int User_Id { get; set; }
        public int Game_Id { get; set; }
        public decimal Balance { get; set; }
        public List<Trade> GetAllTrades { get; set; }

        public decimal ReturnBalance()
        {
            return this.Balance;
        }

    }

    /// <summary>
    /// Model New Portfolio
    /// </summary>
    public class NewPortfolio
    {
        public int Portfolio_Id { get; set; }
        public decimal Balance = 100000M;
        int Game_Id { get; set; }
    }

    public class Leaderboard
    {
        public int holdings_id { get; set; }
        public int Game_Id { get; set; }
        public int[] Places { get; set; }
        public List<User> Players { get; set;  }
        
    }

    public class ActiveStocks
    {
        public int portfolioId { get; set; }
        public string stockSymbol { get; set; }
        public string stockName { get; set; }
        public int quantity { get; set; }

    }

    public class APIStock
    {
        public string symbol { get; set; }
        public decimal price { get; set; }
        public long volume { get; set; }
    }
    
    public class ClosedStock
    {
        public string symbol { get; set; }
        public List<Object> historical { get; set; }
    }
    public class Historical
    {
        public decimal close { get; set; }
    }
}
