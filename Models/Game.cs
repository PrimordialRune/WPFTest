using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GameConsole Console { get; set; }
    }
}
