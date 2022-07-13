using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Windows;

namespace Games.Services
{
    public class GameDBService : IServiceDB<Models.Game>
    {
        public IEnumerable<Models.Game> ExecuteDBQuery(Models.DBSettings settings)
        {
            List<Models.Game> games = new List<Models.Game>();
            try
            {
                using (SqlConnection con = new SqlConnection(settings.ConnString))
                using (SqlCommand command = new SqlCommand(settings.Action, con) { CommandType = CommandType.StoredProcedure })
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    con.Open();
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
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return games;
        }

        public int ExecuteDBDelete(Models.DBSettings settings, int id)
        {
            int result = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(settings.ConnString))
                using (SqlCommand command = new SqlCommand(settings.Action, con) { CommandType = CommandType.StoredProcedure })
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
    }
}
