using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex2_Maze
{
    public interface ITelnetClient : INotifyPropertyChanged
    {
        void Connect(string ip, int port);
        void Send(string command);
        string Read();
        void Disconnect();
        void StartThread();
        void KillThread();

        string playerMove { get; set; }
        Boolean Connected { get; set; }
    }
}
