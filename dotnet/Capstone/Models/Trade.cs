using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Trade
    {
        //public int Trade_Id { get; private set; }
        public int Portfolio_Id { get; private set; }
        public string Stock_Name { get; private set; }
        public string Stock_Symbol { get; private set; }
        public decimal Stock_Price { get; private set; }
        public int Trade_Type { get; private set; } //0 for buy, 1 for sell
        public int Quantity { get; private set; }
        public decimal Total_Trade_Amount
        {
            get
            {
                return Stock_Price * Quantity;
            }
            
        }
        public Trade()
        {

        }

        public Trade(int portfolioId, string stockSymbol, string stockName, decimal stockPrice, int tradeType, int quantity)
        {
            this.Portfolio_Id = portfolioId;
            this.Stock_Name = stockName;
            this.Stock_Symbol = stockSymbol;
            this.Stock_Price = stockPrice;
            this.Trade_Type = tradeType;
            this.Quantity = quantity;
        }
    }

    ///// <summary>
    ///// Model to accept New Trade Parameters
    ///// </summary>
    //public class NewTrade
    //{
    //    public int Portfolio_Id { get; set; }
    //    public string Stock_Symbol { get; set; }
    //    public decimal Stock_Price { get; set; }
    //    public string Trade_Type { get; set; } 
    //    public int Quantity { get; set; }

    //    //public decimal Total_Trade_Amount
    //    //{
    //    //    get
    //    //    {
    //    //        return this.Quantity * this.Stock_Price;
    //    //    }
    //    //}
    //}

    //public class FinishedTrade
    //{
    //    public string Stock_Symbol { get; set; }
    //    public decimal Stock_Price { get; set; }
    //    public string Trade_Type { get; set; }
    //    public int Quantity { get; set; }
    //    public decimal Profit { get; set; }
    //}

}
