using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2_Maze
{
    public class Model : IMazeModel
    {
        volatile Boolean stop;
        ITelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;

        public Model(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
        }

        public void Connect(string ip, int port)
        {
            this.telnetClient.Connect(ip, port);
        }

        public void Disconnect()
        {
            this.telnetClient.Disconnect();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
