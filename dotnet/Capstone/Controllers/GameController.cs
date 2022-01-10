using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        
        private readonly IGameDao gameDao;

        public GameController(IGameDao _gameDao)
        {
            gameDao = _gameDao;
        }

        //role = admin
        [HttpGet]
        public List<Game> GetListOfAllGames()
        {
            return gameDao.GetListOfAllGames();
        }

        [HttpGet("{username}/allgames")]
        public List<Game> GetAllGames(string username)
        {
            return gameDao.GetAllGames(username);
        }

        [HttpGet("{username}/activegames")]
        public List<Game> GetActiveGames(string username)
        {
            return gameDao.GetActiveGames(username);
        }

        [HttpGet("{username}/completedgames")]
        public List<Game> GetCompletedGames(string username)
        {
            return gameDao.GetCompletedGames(username);
        }

        [HttpGet("{username}/organizergames")]
        public List<Game> GetOrganizerGames(string username)
        {
            return gameDao.GetOrganizerGames(username);
        }

        [HttpGet("/game/{gameId}")]
        public Game GetDetailsOfGame(int gameId)
        {
           return gameDao.GetDetailsOfGame(gameId);
        }

        [HttpPost()]
        public int AddNewGame(NewGame game)
        {
            return gameDao.AddNewGame(game);
        }

        [HttpPut("{gameId}/IsComplete")]
        public int updateIsComplete(int gameId)
        {
            return gameDao.UpdateGameStatus(gameId);
        }

        [HttpGet("/leaderboard/{gameId}/{completedDate}")]
        public List<string> GetLeaderboard(int gameId, string completedDate)
        {
            return gameDao.GetLeaderboard(gameId, completedDate);
        }

        //[HttpPost("{username}/game/addplayers")]
        //public int AddPlayersToGame(List<int> userIds, int gameId)
        //{
        //    return gameDao.AddPlayersToGame(userIds, gameId);
        //}

        // TEST  for total sum portfolio (all stockes owned), not final 
        //[HttpGet("/balance")]
        //public List<Portfolio> GetBalancesAllPlayers()
        //{
        //    return gameDao.GetBalancesAllPlayers();
        //}

    }
}
