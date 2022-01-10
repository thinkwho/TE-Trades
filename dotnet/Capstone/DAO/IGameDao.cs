using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IGameDao
    {
        public List<Game> GetListOfAllGames();

        public List<Game> GetAllGames(string username);
        public List<Game> GetActiveGames(string username);
        public List<Game> GetCompletedGames(string username);
        public List<Game> GetOrganizerGames(string username);
        public List<string> GetLeaderboard(int gameId, string completedDate);
        public Game GetDetailsOfGame(int gameId);
        public int AddNewGame(NewGame game);
        public int AddPlayersToGame(List<int> userIds, int gameId);
        public Dictionary<string, decimal> GetGameCurrentActiveStocksValues(int gameId, string completedDate);
        public List<int> GetPortfolioIdsFromGame(int gameId);
        public Dictionary<int, decimal> GetGamePortfoliosAssetBalance(Dictionary<string, decimal> gameStocks, List<int> gamePortfolioIds);
        public decimal GetPlayerTotalStockValue(int portfolioId, Dictionary<string, decimal> gameStocks);
        public decimal GetPlayerBalance(int portfolioId);
        public int UpdateGameAssetBalance(Dictionary<int, decimal> gamePlayersAssetBalances, List<int> portfolios);
        public int UpdateGameStatus(int gameId);

    }
}
