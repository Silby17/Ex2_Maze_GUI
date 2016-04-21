using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex2_Maze
{
    public interface ITelnetClient
    {
        void Connect(string ip, int port);
        void Send(string command);
        string Read();
        void Disconnect();

        Boolean Connected { get; set; }
    }
}
