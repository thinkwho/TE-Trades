using System;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using System.Collections.Generic;
using Capstone.DAO;
using RestSharp;


namespace Capstone.DAO
{
    public class GameSqlDao : IGameDao
    {

        private readonly ReturnUser returnUser = new ReturnUser();
        
        private readonly string connectionString;

        private readonly static string API_URL = "https://financialmodelingprep.com";
        private readonly IRestClient client = new RestClient();


        public GameSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        //////////////////////////////////////////////////////////////
        //------------------------GET-------------------------------//
        //////////////////////////////////////////////////////////////

        public List<Game> GetListOfAllGames()
        {
            List<Game> list = new List<Game>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM games", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Game game = new Game();
                        game.Id = Convert.ToInt32(reader["game_id"]);
                        game.Name = Convert.ToString(reader["game_name"]);
                        game.Start_Date = Convert.ToDateTime(reader["start_date"]);
                        game.End_Date = Convert.ToDateTime(reader["end_date"]);
                        game.Winner = Convert.ToString(reader["winner"]);
                        game.Is_Completed = Convert.ToBoolean(reader["isCompleted"]);
                        list.Add(game);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return list;
        }
       
        public List<Game> GetAllGames(string username)
        {
            List<Game> list = new List<Game>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM games G " +
                                                    "JOIN game_users GU ON G.game_id = GU.game_id " +
                                                    "JOIN users U ON GU.user_id = U.user_id " +
                                                    "WHERE U.username = @username" , conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Game game = new Game();
                        game.Id = Convert.ToInt32(reader["game_id"]);
                        game.Name = Convert.ToString(reader["game_name"]);
                        game.Organizer = Convert.ToString(reader["game_organizer"]);
                        game.Start_Date = Convert.ToDateTime(reader["start_date"]);
                        game.End_Date = Convert.ToDateTime(reader["end_date"]);
                        game.Winner = Convert.ToString(reader["winner"]);
                        game.Is_Completed = Convert.ToBoolean(reader["isCompleted"]);
                        list.Add(game);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return list;
        }
       
        public List<Game> GetActiveGames(string username)
        {
            List<Game> list = new List<Game>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT distinct G.game_id, G.game_name, G.end_date, G.game_organizer, G.start_date, G.winner, G.isCompleted FROM games G " +
                                                    "JOIN game_users GU ON GU.game_id = G.game_id " +
                                                    "JOIN users U ON GU.user_id = GU.user_id " +
                                                    "WHERE U.username = @username " +
                                                    "AND isCompleted = 0;", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Game game = new Game();
                        game.Id = Convert.ToInt32(reader["game_id"]);
                        game.Name = Convert.ToString(reader["game_name"]);
                        game.Organizer = Convert.ToString(reader["game_organizer"]);
                        game.Start_Date = Convert.ToDateTime(reader["start_date"]);
                        game.End_Date = Convert.ToDateTime(reader["end_date"]);
                        game.Winner = Convert.ToString(reader["winner"]);
                        game.Is_Completed = Convert.ToBoolean(reader["isCompleted"]);
                        list.Add(game);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
           
            return list;
        }

        public List<Game> GetCompletedGames(string username)
        {
            List<Game> list = new List<Game>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM games G " +
                                                    "JOIN game_player GP ON G.game_id = GP.game_id " +
                                                    "JOIN users U ON GP.player_id = U.username " +
                                                    "WHERE user_id = @userId " +
                                                    "AND isCompleted = 1", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Game game = new Game();
                        game.Id = Convert.ToInt32(reader["game_id"]);

                        list.Add(game);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return list;
        }

        public List<Game> GetOrganizerGames(string username)
        {
            List<Game> list = new List<Game>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM games " +
                                                    "JOIN _____________ " +
                                                    "WHERE user_id = @userId " +
                                                    "AND role = 1", conn);
                    cmd.Parameters.AddWithValue("@userId", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Game game = new Game();
                        game.Id = Convert.ToInt32(reader["game_id"]);

                        list.Add(game);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return list;
        }

        public Game GetDetailsOfGame(int gameId)
        {
            Game game = new Game();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT game_name, game_organizer, start_date, end_date " +
                                                    "FROM games " +
                                                    "WHERE game_id = @gameId", conn); 
                    
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        game.Name = Convert.ToString(reader["game_name"]);
                        game.Organizer = Convert.ToString(reader["game_organizer"]);
                        game.Start_Date = Convert.ToDateTime(reader["start_date"]);
                        game.End_Date = Convert.ToDateTime(reader["end_date"]);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return game;

        }

        // TEST for total sum portfolio (all stockes owned), not final 
        public List<Portfolio> GetBalancesAllPlayers()
        {
            List<Portfolio> list = new List<Portfolio>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM portfolio", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Portfolio portfolio = new Portfolio();
                        portfolio.User_Id = Convert.ToInt32(reader["player_id"]);
                        portfolio.Game_Id = Convert.ToInt32(reader["game_id"]);
                        portfolio.Balance = Convert.ToDecimal(reader["balance"]);
                        
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }

        //////////////////////////////////////////////////////////////
        //------------------------POST------------------------------//
        //////////////////////////////////////////////////////////////
        public int AddNewGame(NewGame game)
        {

            int rowsAffected = 0;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO games " +
                        "(game_name, game_organizer, start_date, end_date, isCompleted) " +
                        "VALUES (@game_name, @game_organizer, @start_date, @end_date, @isCompleted);", conn);

                    cmd.Parameters.AddWithValue("@game_name", game.Name);
                    cmd.Parameters.AddWithValue("@game_organizer", game.Organizer);
                    cmd.Parameters.AddWithValue("@start_date",  game.Start_Date);
                    cmd.Parameters.AddWithValue("@end_date", game.End_Date);
                    cmd.Parameters.AddWithValue("@isCompleted", 0);

                    rowsAffected += cmd.ExecuteNonQuery();
                    
                    SqlCommand cmdTwo = new SqlCommand("SELECT @@IDENTITY", conn);
                    int newGameId = Convert.ToInt32(cmdTwo.ExecuteScalar());

                    
                    foreach (int player in game.Players)
                    {
                        SqlCommand cmdThree = new SqlCommand("INSERT INTO game_users " +
                        "(user_id, game_id) " + "VALUES (@user_id, @game_id);", conn);

                        cmdThree.Parameters.AddWithValue("@user_id", player);
                        cmdThree.Parameters.AddWithValue("@game_id", newGameId);
                        rowsAffected += cmdThree.ExecuteNonQuery();

                        
                        int newGameUserID = Convert.ToInt32(cmdTwo.ExecuteScalar());
                    
                        SqlCommand cmdFour = new SqlCommand("INSERT INTO portfolios " +
                            "(game_user_id, balance) VALUES (@game_user_id, @balance);", conn);
                        
                        cmdFour.Parameters.AddWithValue("@game_user_id", newGameUserID);
                        cmdFour.Parameters.AddWithValue("@balance", 100000);
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
        
        public int AddPlayersToGame(List<int> userIds, int gameId)
        {
            
            int rowsAffected = 0;
            
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach(int userId in userIds)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO game_player " +
                                                        "(player_id, game_id) " +
                                                        "VALUES " +
                                                        "(@userId, @gameId)", conn);

                        cmd.Parameters.AddWithValue("@userId", userId);
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

        //////////////////////////////////////////////////////////////
        //-------------------------PUT------------------------------//
        //////////////////////////////////////////////////////////////
        public int UpdateGameStatus(int gameId)
        {

            int rowsAffected = 0;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand update = new SqlCommand("UPDATE games " +
                                                        "SET isCompleted = 1 " +
                                                        "WHERE game_id = @gameId", conn);

                    update.Parameters.AddWithValue("@gameId", gameId);

                    rowsAffected = update.ExecuteNonQuery();

                }
            }
            catch (SqlException)
            {
                throw;
            }

            return rowsAffected;

        }

        public int UpdateGameWinner(int gameId)
        {
            
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand update = new SqlCommand("UPDATE games " +
                                                        "SET winner = @username " +
                                                        "WHERE game_id = @gameId " +
                                                        "AND ", conn);

                    
                    update.Parameters.AddWithValue("@gameId", gameId);

                    rowsAffected = update.ExecuteNonQuery();

                }
            }
            catch (SqlException)
            {
                throw;
            }

            return rowsAffected;
        }

        public int UpdateGameBalance(int gameId)
        {

            int rowsAffected = 0;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand update = new SqlCommand("UPDATE balance " +
                                                        "FROM ________ " +
                                                        "JOIN ________ " +
                                                        "WHERE user_id = @userId", conn);

                    rowsAffected = update.ExecuteNonQuery();

                }

            }
            catch (SqlException)
            {
                throw;
            }

            return rowsAffected;

        }

        public List<string> GetLeaderboard(int gameId, string completedDate)
        {
            List<string> leaderboard = new List<string>();
            Dictionary<string, decimal> gameStocks = GetGameCurrentActiveStocksValues(gameId, completedDate);
            List<int> gamePortfolioIds = GetPortfolioIdsFromGame(gameId);
            Dictionary<int, decimal> GamePortfoliosAssetBalance = GetGamePortfoliosAssetBalance(gameStocks, gamePortfolioIds);
            UpdateGameAssetBalance(GamePortfoliosAssetBalance, gamePortfolioIds);
            

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT asset_balance, username FROM portfolios P " +
                                                   "JOIN game_users GU ON GU.game_user_id = P.game_user_id " +
                                                   "JOIN games G ON G.game_id = GU.game_id " +
                                                   "JOIN users U ON U.user_id = GU.user_id " +
                                                   "WHERE G.game_id = @gameId " +
                                                   "ORDER BY asset_balance DESC", conn);
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        leaderboard.Add(Convert.ToString(reader["username"]) + "," + Convert.ToString(reader["asset_balance"]));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            
            
            return leaderboard;
        }

        public int UpdateGameAssetBalance(Dictionary<int, decimal> gamePortfolioAssetBalance, List<int> portfolios)
        {
            int rowsAffected = 0;
            

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    for(int i = 0; i < portfolios.Count; i++)
                    {
                        decimal assetBalance = gamePortfolioAssetBalance[portfolios[i]];
                        SqlCommand cmd = new SqlCommand("UPDATE portfolios " +
                                                        "SET asset_balance = @assetBalance " +
                                                        "WHERE portfolio_id = @portfolio_id;", conn);
                        cmd.Parameters.AddWithValue("@assetBalance", assetBalance);
                        cmd.Parameters.AddWithValue("@portfolio_id", portfolios[i]);

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

        public Dictionary<int, decimal> GetGamePortfoliosAssetBalance(Dictionary<string, decimal> gameStocks, List<int> gamePortfolioIds)
        {
            decimal playerStockValue = 0;
            decimal playerCashBalance = 0;
            Dictionary<int, decimal> gamePortfoliosAssetBalance = new Dictionary<int, decimal>();

            for(int i = 0; i < gamePortfolioIds.Count; i++)
            {
                playerStockValue = GetPlayerTotalStockValue(gamePortfolioIds[i], gameStocks);
                playerCashBalance = GetPlayerBalance(gamePortfolioIds[i]);
                gamePortfoliosAssetBalance.Add(gamePortfolioIds[i], playerStockValue + playerCashBalance);
            }


            return gamePortfoliosAssetBalance;
        }

        public decimal GetPlayerBalance(int portfolioId)
        {
            decimal playerBalance = 0;

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT balance FROM portfolios WHERE portfolio_id = @portfolioId;", conn);
                    cmd.Parameters.AddWithValue("@portfolioId", portfolioId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        playerBalance = Convert.ToDecimal(reader["balance"]);
                    }

                }
            }
            catch(SqlException)
            {
                throw;
            }

            return playerBalance;
        }

        public decimal GetPlayerTotalStockValue(int portfolioId, Dictionary<string, decimal> gameStocks)
        {
            decimal totalStockValue = 0;
            IPortfolioDao portfolioDao = new PortfolioSqlDao(connectionString);
            List<ActiveStocks> activeStocks = portfolioDao.GetAllActiveStocks(portfolioId);

            foreach(ActiveStocks stock in activeStocks)
            {
                totalStockValue += (stock.quantity * gameStocks[stock.stockSymbol]);
            }

            return totalStockValue;
        }


        public Dictionary<string, decimal> GetGameCurrentActiveStocksValues(int gameId, string completedDate)
        {
            
            string activeStocksString = "";
            string stockAPIKey = "apikey=e1abf071b1023e06176404e2e7c089ef";
            //string stockAPIKey = "apikey=ea8ccc2fe0b59471e03ff6a6d4ffdd5c";
            List<APIStock> gameStockInfo = new List<APIStock>();
            Dictionary<string, decimal> stockPrices = new Dictionary<string, decimal>();
            

            try
            {

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT stock_symbol FROM active_stocks [AS] " +
                                                    "JOIN portfolios P ON [AS].portfolio_id = P.portfolio_id " +
                                                    "JOIN game_users GU ON P.game_user_id = GU.game_user_id " +
                                                    "WHERE GU.game_user_id = P.game_user_id AND GU.game_id = @gameId;", conn) ;
                    cmd.Parameters.AddWithValue("@gameId", gameId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        activeStocksString += Convert.ToString(reader["stock_symbol"]) + ",";
                    }
                }
               
            }
            catch (SqlException)
            {
                throw;
            }

            if(activeStocksString == "")
            {
                Dictionary<string, decimal> NoActiveStocks = new Dictionary<string, decimal>();
                NoActiveStocks.Add("", 0);
                return NoActiveStocks;
            }

            if (completedDate == "yuppie")
            {
                RestRequest request = new RestRequest($"{API_URL}/api/v3/quote-short/{activeStocksString}?{stockAPIKey}");
                IRestResponse<List<APIStock>> response = client.Get<List<APIStock>>(request);

                if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
                {
                    throw new Exception();
                }
                else
                {
                    gameStockInfo = response.Data;
                }

                foreach (APIStock stock in gameStockInfo)
                {
                    stockPrices.Add(stock.symbol, stock.price);
                }
            }
            else 
            {
                string[] activeStockArr = activeStocksString.Split(',');
                ClosedStock gameClosedStockInfo = new ClosedStock();
                for (int i = 0; i < activeStockArr.Length-1; i++)
                {
                    RestRequest request = new RestRequest($"{API_URL}/api/v3/historical-price-full/{activeStockArr[i]}?to={completedDate}&{stockAPIKey}");
                    IRestResponse<ClosedStock> response = client.Get<ClosedStock>(request);

                    if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        gameClosedStockInfo = response.Data;
                    }

                    //string obj = s.historical.Substring(1, s.historical.Length-2);
                    //Historical temp = JObject.Parse(obj);
                    //Historical t = JsonSerializer.Deserialize<Historical>(obj);
                    //Historical hist = JsonSerializer.Deserialize<Historical>()
                    Dictionary<string, Object> historicalDictionary = (Dictionary<string, Object>)gameClosedStockInfo.historical[0];
                    double close = (double)historicalDictionary["close"];
                    stockPrices.Add(gameClosedStockInfo.symbol, (decimal)close);
                    
                }
            }

            


            return stockPrices;
        }

        public List<int>GetPortfolioIdsFromGame(int gameId)
        {
            List<int> gamePortfolioIds = new List<int>();

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmdTwo = new SqlCommand("SELECT portfolio_id FROM portfolios P " +
                                                       "JOIN game_users GU ON P.game_user_id = GU.game_user_id " +
                                                       "WHERE GU.game_id = @gameId;", conn);
                    cmdTwo.Parameters.AddWithValue("@gameId", gameId);
                    SqlDataReader reader = cmdTwo.ExecuteReader();
                    while (reader.Read())
                    {
                        gamePortfolioIds.Add(Convert.ToInt32(reader["portfolio_id"]));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return gamePortfolioIds;
        }
    }

}
