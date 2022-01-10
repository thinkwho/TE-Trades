using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public class TradeSqlDao : ITradeDao
    {

        private readonly string connectionString;

        public TradeSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int ExecuteTradeBuy(int Portfolio_Id, string Stock_Symbol, string Stock_Name, decimal Stock_Price, int Trade_Type, int Quantity, decimal Total_Trade_Amount)
        //public int ExecuteTradeBuy(Trade trade)
        {
            int rowsAffected = 0;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    SqlCommand cmd = new SqlCommand("INSERT INTO trades (portfolio_id, stock_symbol, stock_name, stock_price, trade_type, quantity) " +
                                                    "VALUES (@Portfolio_Id, @Stock_Symbol, @Stock_Name, @Stock_Price, @Trade_Type, @Quantity)", conn);

                    cmd.Parameters.AddWithValue("@Portfolio_Id", Portfolio_Id);
                    cmd.Parameters.AddWithValue("@Stock_Symbol", Stock_Symbol);
                    cmd.Parameters.AddWithValue("@Stock_Name", Stock_Name);
                    cmd.Parameters.AddWithValue("@Stock_Price", Stock_Price);
                    cmd.Parameters.AddWithValue("@Trade_Type", Trade_Type);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);

                    rowsAffected += cmd.ExecuteNonQuery();
                    
                    SqlCommand cmdTwo = new SqlCommand("UPDATE portfolios " +
                                                        "SET balance -= @tradeAmount " +
                                                        "WHERE portfolio_id = @portfolioId", conn);
                    cmdTwo.Parameters.AddWithValue("@tradeAmount", Total_Trade_Amount);
                    cmdTwo.Parameters.AddWithValue("@portfolioId", Portfolio_Id);

                    rowsAffected += cmdTwo.ExecuteNonQuery();

                   
                    if(!isStockSymbolPresent(Stock_Symbol, Portfolio_Id))
                    {
                        SqlCommand cmdThree = new SqlCommand("INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) " +
                                                             "VALUES (@portfolio_id, @stock_symbol, @stock_name, @quantity)", conn);
                        cmdThree.Parameters.AddWithValue("@portfolio_id", Portfolio_Id);
                        cmdThree.Parameters.AddWithValue("@stock_symbol", Stock_Symbol);
                        cmdThree.Parameters.AddWithValue("@stock_name", Stock_Name);
                        cmdThree.Parameters.AddWithValue("@quantity", Quantity);

                        rowsAffected += cmdThree.ExecuteNonQuery();

                    }
                    else
                    {
                        SqlCommand cmdFour = new SqlCommand("UPDATE active_stocks SET quantity += @quantity WHERE portfolio_id = @portfolio_id AND stock_symbol = @stock_symbol", conn);
                        cmdFour.Parameters.AddWithValue("@quantity", Quantity);
                        cmdFour.Parameters.AddWithValue("@portfolio_id", Portfolio_Id);
                        cmdFour.Parameters.AddWithValue("@stock_symbol", Stock_Symbol);

                        rowsAffected += cmdFour.ExecuteNonQuery();

                    }

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return rowsAffected;
        }

        public int ExecuteTradeSell(int Portfolio_Id, string Stock_Symbol, string Stock_Name, decimal Stock_Price, int Trade_Type, int Quantity, decimal Total_Trade_Amount)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (!isStockSymbolPresent(Stock_Symbol, Portfolio_Id))
                    {
                        return 0;

                    }
                    else
                    {
                        SqlCommand cmdFour = new SqlCommand("UPDATE active_stocks SET quantity -= @quantity WHERE portfolio_id = @portfolio_id AND stock_symbol = @stock_symbol", conn);
                        cmdFour.Parameters.AddWithValue("@quantity", Quantity);
                        cmdFour.Parameters.AddWithValue("@portfolio_id", Portfolio_Id);
                        cmdFour.Parameters.AddWithValue("@stock_symbol", Stock_Symbol);

                        rowsAffected += cmdFour.ExecuteNonQuery();

                    }



                    SqlCommand cmd = new SqlCommand("INSERT INTO trades (portfolio_id, stock_symbol, stock_name, stock_price, trade_type, quantity) " +
                                                    "VALUES (@Portfolio_Id, @Stock_Symbol, @Stock_Name, @Stock_Price, @Trade_Type, @Quantity)", conn);

                    cmd.Parameters.AddWithValue("@Portfolio_Id", Portfolio_Id);
                    cmd.Parameters.AddWithValue("@Stock_Symbol", Stock_Symbol);
                    cmd.Parameters.AddWithValue("@Stock_Name", Stock_Name);
                    cmd.Parameters.AddWithValue("@Stock_Price", Stock_Price);
                    cmd.Parameters.AddWithValue("@Trade_Type", Trade_Type);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);

                    rowsAffected += cmd.ExecuteNonQuery();

                    SqlCommand cmdTwo = new SqlCommand("UPDATE portfolios " +
                                                        "SET balance += @tradeAmount " +
                                                        "WHERE portfolio_id = @portfolioId", conn);
                    cmdTwo.Parameters.AddWithValue("@tradeAmount", Total_Trade_Amount);
                    cmdTwo.Parameters.AddWithValue("@portfolioId", Portfolio_Id);

                    rowsAffected += cmdTwo.ExecuteNonQuery();

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return rowsAffected;
        }

        private bool isStockSymbolPresent(string symbol, int portfolio_id)
        {
            List<string> stocks = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT stock_symbol FROM active_stocks " +
                                                    "WHERE stock_symbol = @symbol AND portfolio_id = @portfolio_id;", conn);
                    cmd.Parameters.AddWithValue("@symbol", symbol);
                    cmd.Parameters.AddWithValue("@portfolio_id", portfolio_id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        stocks.Add(symbol);
                    }
                }
            }catch(SqlException)
            {
                throw;
            }
            return stocks.Count == 1;
        }

       /* private bool isPortfolioIdPresent(int id)
        {
            List<string> ids = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT portfolio_id FROM active_stocks " +
                                                    "WHERE portfolio_id = @id;");
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ids.Add(id);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return ids.Count == 1;
        }
       */
    }
}
