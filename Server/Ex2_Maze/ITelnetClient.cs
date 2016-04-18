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
        void Write(string commnad);
        string Read();
        void Disconnect();
    }
}
