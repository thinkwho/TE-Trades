using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class PortfolioSqlDao : IPortfolioDao
    {
        private readonly ReturnUser ReturnUser = new ReturnUser();

        private readonly string connectionString;

        public PortfolioSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int GetPortfolioId(int gameId, string username)
        {
            int portfolioId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT portfolio_id FROM portfolios P " +
                                                    "JOIN game_users GU ON GU.game_user_id = P.game_user_id " +
                                                    "JOIN users U ON U.user_id = GU.user_id " +
                                                    "JOIN games G ON G.game_id = GU.game_id " +
                                                    "WHERE U.username = @username " +
                                                    "AND G.game_id = @gameId", conn);

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        portfolioId = Convert.ToInt32(reader["portfolio_id"]);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return portfolioId;
        }

        public List<ActiveStocks> GetAllActiveStocks(int portfolioId)
        {

            List<ActiveStocks> activeStocksList = new List<ActiveStocks>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM active_stocks " +
                                                    "WHERE portfolio_id = @portfolioId;"
                                                   , conn);

                    cmd.Parameters.AddWithValue("@portfolioId", portfolioId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ActiveStocks newActiveStocks = new ActiveStocks();
                        newActiveStocks = GetAccountFromReader(reader);
                        activeStocksList.Add(newActiveStocks);
                    }

                    //while (reader.Read())
                    //{
                    //    GetAccountFromReader()
                    //}
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return activeStocksList;
        }
        public decimal GetGameBalance(string username, int gameId)
        {
            decimal balance = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT P.balance " +
                                                    "FROM portfolios P " +
                                                    "JOIN game_users GU ON P.game_user_id = GU.game_user_id " +
                                                    "JOIN games G ON GU.game_id = G.game_id " +
                                                    "JOIN users U ON GU.user_id = U.user_id " +
                                                    "WHERE U.username = @username " +
                                                    "AND G.game_id = @gameId", conn);

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        balance = Convert.ToDecimal(reader["balance"]);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return balance;
        }

        public int AddNewPortfolio(List<int> userIds, int gameId)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    foreach (int id in userIds)
                    {
                        cmd.CommandText = "INSERT INTO portfolios " +
                                          "(player_id, game_id, balance) " +
                                          "VALUES " +
                                          "(@userId, @gameId, 100000)";
                        cmd.Parameters.AddWithValue("@userId", ReturnUser.UserId);
                        cmd.Parameters.AddWithValue("@gameId", gameId);

                        rowsAffected += cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return rowsAffected;
        }

        public int UpdateBalance(int gameId, decimal amount)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE P " +
                                                    "SET balance = @amount " +
                                                    "FROM portfolios P " +
                                                    "JOIN users U ON P.user_id = U.user_id " +
                                                    "JOIN games G ON P.player_id = G.game_id " +
                                                    "WHERE U.user_id = @userId " +
                                                    "AND P.game_id = @gameId", conn);

                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@userId", ReturnUser.UserId);
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    rowsAffected += cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return rowsAffected;
        }

        private ActiveStocks GetAccountFromReader(SqlDataReader reader)
        {
           ActiveStocks activeStocks = new ActiveStocks()
            {
                portfolioId = Convert.ToInt32(reader["portfolio_id"]),
                stockSymbol = Convert.ToString(reader["stock_symbol"]),
                stockName = Convert.ToString(reader["stock_name"]),
                quantity = Convert.ToInt32(reader["quantity"]),
           };

            return activeStocks;
        }

        //goal of this method is to return A player in A specific game entire history of trades
        //public List<Portfolio> GetAllTrades(int Portfolio_Id)
        //{
        //    return = null;
        //}
    }
}