using Games.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Games.ViewModels.fromModels
{
    public class GameViewModel : BaseViewModel
    {
        private Models.Game game = null;

        public GameViewModel(Models.Game game)
        {
            this.game = game;
        }

        public int ID { get => game.Id; set { game.Id = value; Notify(); }}
        public string Name { get => game.Name; set { game.Name = value; Notify(); }}
        public string ReleaseDate { get => game.ReleaseDate.ToString("dd/MM/yyyy"); set { game.ReleaseDate = DateTime.Parse(value); Notify(); } }
        public Models.GameConsole Console { get => game.Console; set { game.Console = value; Notify(); } }

    }

}
