using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Models
{
    public class GameConsole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ConsoleBrand Brand { get; set; }

    }
}
