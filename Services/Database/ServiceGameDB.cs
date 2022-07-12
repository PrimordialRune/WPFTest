using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Games.Services
{
    public class ServiceGameDB : IServiceDB
    {
        public bool ExecuteDBAction(Models.DBSettings settings)
        {
            using (SqlConnection con = new SqlConnection(settings.ConnString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(settings.Action, con))
                {
                    //command.Parameters.AddWithValue("@ID", SelectedItem.ID);
                    //command.ExecuteNonQuery();
                }
                con.Close();
            }
            return true;
        }
    }
}
