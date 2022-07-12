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
        public readonly IHost host;
        public static IServiceProvider ServiceProvider { get; private set; }

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

        private void DeleteMethod(object param) { deleteGameRow(); Games.Remove(SelectedItem); }
        private bool DeleteCanExec(object param) { return SelectedItem != null; }

        private void AddMethod(object param)
        {
            var mainWindow = ServiceProvider.GetRequiredService<Views.AddGameWindowView>();
            mainWindow.Show();
            /*Views.AddGameWindowView GameView = new Views.AddGameWindowView();
            GameView.DataContext = new ViewModels.AddGameWindowViewModel(service, settings, SelectedPlatform);
            GameView.ShowDialog();*/
        }

        private ObservableCollection<fromModels.GameViewModel> games = null;
        public ObservableCollection<fromModels.GameViewModel> Games  { get => games; private set { games = value; Notify();} }
        
        public static List<Models.Game> StagedGames { get; protected set; }
        
        public RelayCommand RemoveGame { get; private set; }
        public RelayCommand AddGame { get; private set; }
        
        public ObservableCollection<fromModels.GameConsoleViewModel> Consoles { get; private set; }
        
        protected string connString = "Data Source=TIQ-STAGE;Initial Catalog=games;Integrated Security=True";

        public List<Models.Game> queryGames(string query)
        {
            var games = new List<Models.Game>();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        games.Add(new Models.Game()
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("GameID")),
                            Name = dr.GetString(dr.GetOrdinal("Title")),
                            ReleaseDate = dr.GetDateTime(dr.GetOrdinal("Release_Date")),
                            Console = new Models.GameConsole()
                            {
                                Id = dr.GetInt32(dr.GetOrdinal("ConsoleID")),
                                Name = dr.GetString(dr.GetOrdinal("ConsoleName")),
                                Brand = new Models.ConsoleBrand() 
                                {
                                    Id=dr.GetInt16(dr.GetOrdinal("BrandID")),
                                    Name=dr.GetString(dr.GetOrdinal("BrandName"))
                                }
                            }
                        });
                    }
                }
                con.Close();
                return games;
            }
        }

        public void deleteGameRow()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Games] WHERE ID = @ID", con))
                {
                    command.Parameters.AddWithValue("@ID", SelectedItem.ID);
                    command.ExecuteNonQuery();
                }
                StagedGames = queryGames(queryG);
                con.Close();
            }
        }
        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<Models.DBSettings>(configuration.GetSection(nameof(Models.DBSettings)));
            services.AddScoped<Services.IServiceDB, Services.ServiceGameDB>();
            services.AddSingleton<ViewModels.AddGameWindowViewModel>();
            services.AddTransient<Views.AddGameWindowView>();
        }

        protected string queryG = "SELECT [g].[ID] AS [GameID], [g].[Title], [g].[Release_Date], [gp].[ConsoleID], [c].[Name] AS [ConsoleName], [c].[BrandID], [c].[BrandID], [br].[Name] AS [BrandName]" +
                                "FROM [dbo].[Games] AS [g] JOIN [dbo].[_GamePlatforms] AS [gp] ON [g].[ID] = [gp].[GameID] JOIN [dbo].[Consoles] AS [c] ON [gp].[ConsoleID] = [c].[ID] JOIN [dbo].[Brands] AS [br] ON [c].[BrandID] = [br].[ID]";

        private readonly Services.IServiceDB service;
        private readonly Models.DBSettings settings;
        public MainWindowViewModel( Services.IServiceDB service, IOptions<Models.DBSettings> options)
        {
            host = Host.CreateDefaultBuilder().ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); }).Build();
            ServiceProvider = host.Services;
            this.service = service;
            settings = options.Value;
            Title = InDesignMode ? "Design Mode" : "Console Games";
            
            StagedGames = queryGames(queryG);
            Consoles = new ObservableCollection<fromModels.GameConsoleViewModel>(StagedGames.Select(g => new fromModels.GameConsoleViewModel(g.Console)).GroupBy(c => c.ID, (key, c) => c.FirstOrDefault()).ToList());
            RemoveGame = new RelayCommand(DeleteMethod, DeleteCanExec);
            SelectedItem = null;
            AddGame = new RelayCommand(AddMethod);
        }
        private Task ExecuteAsync()
        {
            var mainWindow = ServiceProvider.GetRequiredService<Views.AddGameWindowView>();
            mainWindow.Show();
            return Task.CompletedTask;
        }
    }
}