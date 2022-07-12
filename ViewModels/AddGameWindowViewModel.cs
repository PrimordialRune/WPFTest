using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Games.ViewModels
{
    public class AddGameWindowViewModel
    {
        public AddGameWindowViewModel(fromModels.GameConsoleViewModel cmodel)
        {
            //SelectedItem.Console = cmodel.GetConsoleModel();
        }

        /*public void AddMethod(object param)
        {
            if (!String.IsNullOrEmpty(SelectedItem.Name))
            {
                AddGameRow(SelectedItem);
            }
        }

        public void AddGameRow(fromModels.GameViewModel game)
        {
            int gameID;
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Games](Title, Release_Date, Publisher, Engine) VALUES(@Name, @ReleaseDate, NTD02341, null)", con))
                {
                    command.Parameters.AddWithValue("@Name", SelectedItem.Name);
                    command.Parameters.AddWithValue("@ReleaseDate", SelectedItem.ReleaseDate);
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM [dbo].[Games] ORDER BY ID DESC", con))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        dr.Read();
                        gameID = dr.GetInt32(dr.GetOrdinal("ID"));
                    }
                }
                using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[_GamePlatform](GameID, ConsoleID) VALUES(@GameID, @ConsoleID)", con))
                {
                    command.Parameters.AddWithValue("@GameID", gameID);
                    command.Parameters.AddWithValue("@ConsoleID", SelectedItem.Console.Id);
                    command.ExecuteNonQuery();
                }
                StagedGames = queryGames(queryG);
                con.Close();
            }
        }*/

    }
}
