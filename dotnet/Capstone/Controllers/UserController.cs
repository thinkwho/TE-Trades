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
    public class UsersController : ControllerBase
    {

        private readonly IUserDao userDao;
        public UsersController(IUserDao _userDao)
        {
            userDao = _userDao;
        }

        [HttpGet("/allusers")]
        public List<User> GetAllUsers()
        {
            return userDao.GetAllUsers();
        }
   
    }
}
