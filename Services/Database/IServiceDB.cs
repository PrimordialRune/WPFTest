using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Services
{
    public interface IServiceDB<T> where T : class
    {
        IEnumerable<T> ExecuteDBQuery(Models.DBSettings settings);
        int ExecuteDBDelete(Models.DBSettings settings, int id);
    }
}
