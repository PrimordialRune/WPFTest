using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.ViewModels
{
    public class AddGameWindowViewModel : MainWindowViewModel, Navigation.IActivable
    {
        public AddGameWindowViewModel(Navigation.NavigationService navigationService, Services.IServiceDB<Models.Game> DBservice) : base(navigationService,DBservice)
        {
            AddGame = new RelayCommand(AddMethod);
            SelectedItem = new fromModels.GameViewModel(new Models.Game());
            SelectedPlatform = new fromModels.GameConsoleViewModel(new Models.GameConsole());
            
        }
        public Task ActivateAsync(object parameter)
        {
            SelectedPlatform = (fromModels.GameConsoleViewModel) parameter;
            return Task.CompletedTask;
        }

        public void AddMethod(object param)
        {
            if (!String.IsNullOrEmpty(SelectedItem.Name))
            {
                SelectedItem.Console = SelectedPlatform.GetConsoleModel();
                DBservice.SetAction("insertGame");
                DBservice.ExecuteDBInsert(SelectedItem);
                DBservice.SetAction("queryGames");
                StagedGames = DBservice.ExecuteDBQuery().ToList();
                Games.Add(SelectedItem);
                //TODO Close Window On Insert
                //await navigationService.CloseAsync(Navigation.Windows.AddGameWindow);
            }

        }

    }
}
