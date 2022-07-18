using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;

namespace Games.Services
{
    public class GameDBService : IServiceDB<Models.Game>
    {
        private readonly IServiceProvider serviceProvider;
        public Models.DBSettings Settings { get; set; }
        public IEnumerable<Models.Game> ExecuteDBQuery()
        {
            List<Models.Game> games = new List<Models.Game>();
            try
            {
                using (SqlConnection con = new SqlConnection(Settings.ConnString))
                using (SqlCommand command = new SqlCommand(Settings.Action, con) { CommandType = CommandType.StoredProcedure })
                {
                    con.Open();
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
                                        Id = dr.GetInt16(dr.GetOrdinal("BrandID")),
                                        Name = dr.GetString(dr.GetOrdinal("BrandName"))
                                    }
                                }
                            });
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return games;
        }
        public int ExecuteDBDelete(int id)
        {
            int result = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(Settings.ConnString))
                using (SqlCommand command = new SqlCommand(Settings.Action, con) { CommandType = CommandType.StoredProcedure })
                {
                    con.Open();
                    command.Parameters.AddWithValue("@Id", id);
                    result = command.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return result;
        }
        public int ExecuteDBInsert(object parameter)
        {
            int result = -1;
            ViewModels.fromModels.GameViewModel game = (ViewModels.fromModels.GameViewModel) parameter;
            try
            {
                using (SqlConnection con = new SqlConnection(Settings.ConnString))
                using (SqlCommand command = new SqlCommand(Settings.Action, con) { CommandType = CommandType.StoredProcedure })
                {
                    con.Open();
                    command.Parameters.AddWithValue("@Name", game.Name);
                    command.Parameters.AddWithValue("@ReleaseDate", game.ReleaseDate);
                    command.Parameters.AddWithValue("@ConsoleID", game.Console.Id);
                    result = command.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return result;
        }

        public void SetAction(string action)
        {
            Settings.Action = action;
        }
        public void Configure(string connectionString)
        {
            Settings = new Models.DBSettings();
            Settings.ConnString = connectionString;
        }
        public GameDBService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
    }
}
