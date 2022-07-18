using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Games.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string Title { get; set; }
        protected Navigation.NavigationService navigationService;
        protected Services.IServiceDB<Models.Game> DBservice;
        public RelayCommand ExecuteCommand { get; }
        private fromModels.GameConsoleViewModel selectedPlatform = null;
        public fromModels.GameConsoleViewModel SelectedPlatform
        {
            get => selectedPlatform;
            set
            {
                if (selectedPlatform == value) { return; }
                selectedPlatform = value;
                PlatformSelected = true;
                Games = new ObservableCollection<fromModels.GameViewModel>(StagedGames.Select(g => new fromModels.GameViewModel(g)).Where( g => g.Console.Name == selectedPlatform.Name ).ToList());
                SearchText = "";
                Notify();
            }
        }
        private bool platformSelected = false;
        public bool PlatformSelected { get => platformSelected; private set { platformSelected = value; Notify(); } }
        private fromModels.GameViewModel selectedItem = null;
        public fromModels.GameViewModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem == value) { return; }
                selectedItem = value;
                Notify();
                RemoveGame.RaiseCanExecuteChanged();
            }
        }
        private string searchText = "";
        public string SearchText 
        {
            get => searchText;
            set
            {
                searchText = value;
                Notify();
                Games = new ObservableCollection<fromModels.GameViewModel>(StagedGames.Select(g => new fromModels.GameViewModel(g)).Where(g => g.Name.ToUpper().StartsWith(searchText.ToUpper())).Where(g => g.Console.Name == selectedPlatform.Name).ToList()) ;
            }
        }
        private void DeleteMethod(object param) 
        {
            DBservice.SetAction("deleteGame");
            DBservice.ExecuteDBDelete(SelectedItem.ID);
            DBservice.SetAction("queryGames");
            StagedGames = DBservice.ExecuteDBQuery().ToList();
            Games.Remove(SelectedItem);
        }
        /*private void UpdateMethod(object param)
        {
            DBservice.SetAction("queryGames");
            StagedGames = DBservice.ExecuteDBQuery().ToList();
            Games = new ObservableCollection<fromModels.GameViewModel>(StagedGames.Select(g => new fromModels.GameViewModel(g)).ToList());
        }*/
        private bool DeleteCanExec(object param) { return SelectedItem != null; }
        private void AddMethod(object param)
        {
            ShowDialogAsync();
        }
        private Task ShowDialogAsync()
        {
            return navigationService.ShowDialogAsync(Navigation.Windows.AddGameWindow, SelectedPlatform);
        }

        private ObservableCollection<fromModels.GameViewModel> games = null;
        public ObservableCollection<fromModels.GameViewModel> Games  { get => games; private set { games = value; Notify();} }
        public List<Models.Game> StagedGames { get; set; }
        public RelayCommand RemoveGame { get; private set; }
        public RelayCommand AddGame { get; protected set; }
        //public RelayCommand UpdateGame { get; protected set; }
        public ObservableCollection<fromModels.GameConsoleViewModel> Consoles { get; private set; }
        public MainWindowViewModel(Navigation.NavigationService navigationService, Services.IServiceDB<Models.Game> DBservice)
        {
            Title = InDesignMode ? "Design Mode" : "Console Games";
            this.navigationService = navigationService;
            this.DBservice = DBservice;
            DBservice.SetAction("queryGames");
            StagedGames = DBservice.ExecuteDBQuery().ToList();
            Consoles = new ObservableCollection<fromModels.GameConsoleViewModel>(StagedGames.Select(g => new fromModels.GameConsoleViewModel(g.Console)).GroupBy(c => c.ID, (key, c) => c.FirstOrDefault()).ToList());
            RemoveGame = new RelayCommand(DeleteMethod, DeleteCanExec);
            SelectedItem = null;
            AddGame = new RelayCommand(AddMethod);
            //UpdateGame = new RelayCommand(UpdateMethod);
        }
    }
}