using System;
using System.Collections.Generic;
using System.Text;

namespace Games.ViewModels.fromModels
{
    public class GameConsoleViewModel : BaseViewModel
    {
        private Models.GameConsole console = null;

        public GameConsoleViewModel(Models.GameConsole console)
        {
            this.console = console;
        }

        public int ID { get => console.Id; set { console.Id = value; Notify(); } }
        public string Name { get => console.Name; set { console.Name = value; Notify(); } }
        public DateTime ReleaseDate { get => console.ReleaseDate; set { console.ReleaseDate = value; Notify(); } }
        public Models.ConsoleBrand Brand { get => console.Brand; set { console.Brand = value; Notify(); } }
        public Models.GameConsole GetConsoleModel()
        {
            return console;
        }
    }
}
