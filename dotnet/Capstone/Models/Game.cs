using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Organizer { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string[] ListOfPlayers { get; set; }
        public string Winner { get; set; }
        public bool Is_Completed { get; set; }
    }

    /// <summary>
    /// Model to accept New Game parameters
    /// </summary>
    public class NewGame
    {
        public string Name { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int[] Players { get; set; }
        public string Organizer { get; set; }
    }


}
