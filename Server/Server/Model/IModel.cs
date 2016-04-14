using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    public delegate void NewModelChange(Object source, EventArgs e);

    public interface IModel
    {
        event NewModelChange newModelChange;
        void ExecuteCommandalbe(List<object> list, Socket client);
        void CreateOptionsDictionary();
        void PublishEvent();
        string GetModelChange();
        void SendToClient(string msg, Socket client);
    }
}
