using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2_Maze
{
    public interface ITelnetClient
    {
        void Connect(string ip, int port);
        void Start();
        void Send(string command);
        void Receive();
        void Disconnect();
    }
}
