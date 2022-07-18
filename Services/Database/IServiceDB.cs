using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Games.Services
{
    public interface IServiceDB<T> where T : class
    {
        Models.DBSettings Settings { get; set; }
        IEnumerable<T> ExecuteDBQuery();
        int ExecuteDBDelete(int id);
        int ExecuteDBInsert(object parameter);
        void SetAction(string action);
    }
}
