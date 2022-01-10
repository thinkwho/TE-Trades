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
    [Route("/")]
    [ApiController]
    [Authorize]
    public class TradeController : ControllerBase
    {

        private readonly ITradeDao tradeDao;

        public TradeController(ITradeDao _tradeDao)
        {
            tradeDao = _tradeDao;
        }

        
        [HttpPost("executetradebuy")]
        
        /*public void ExecuteTradeBuy(Trade trade)
        {
            tradeDao.ExecuteTradeBuy(trade);
        }*/

       
        [HttpPost("executetradebuy/{Portfolio_Id}/{Stock_Symbol}/{Stock_Name}/{Stock_Price}/{Trade_Type}/{Quantity}/{Total_Trade_Amount}")]
        public void ExecuteTradeBuy(int Portfolio_Id, string Stock_Symbol, string Stock_Name, decimal Stock_Price, int Trade_Type, int Quantity, decimal Total_Trade_Amount)
        {
            tradeDao.ExecuteTradeBuy(Portfolio_Id, Stock_Symbol, Stock_Name, Stock_Price, Trade_Type, Quantity, Total_Trade_Amount);
        }

        [HttpPost("executetradesell/{Portfolio_Id}/{Stock_Symbol}/{Stock_Name}/{Stock_Price}/{Trade_Type}/{Quantity}/{Total_Trade_Amount}")]
        public void ExecuteTradeSell(int Portfolio_Id, string Stock_Symbol, string Stock_Name, decimal Stock_Price, int Trade_Type, int Quantity, decimal Total_Trade_Amount)
        {
            tradeDao.ExecuteTradeSell(Portfolio_Id, Stock_Symbol, Stock_Name, Stock_Price, Trade_Type, Quantity, Total_Trade_Amount);
        }

    }
}
