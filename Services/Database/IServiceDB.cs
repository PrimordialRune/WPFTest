using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Services
{
    public interface IServiceDB
    {
        bool ExecuteDBAction(Models.DBSettings settings);
    }
}
