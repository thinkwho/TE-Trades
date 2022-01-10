using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ITradeDao
    {
        //public int ExecuteTradeBuy(Trade trade);
        public int ExecuteTradeBuy(int Portfolio_Id, string Stock_Symbol, string Stock_Name, decimal Stock_Price, int Trade_Type, int Quantity, decimal Total_Trade_Amount);
        public int ExecuteTradeSell(int Portfolio_Id, string Stock_Symbol, string Stock_Name, decimal Stock_Price, int Trade_Type, int Quantity, decimal Total_Trade_Amount);
    }
}
