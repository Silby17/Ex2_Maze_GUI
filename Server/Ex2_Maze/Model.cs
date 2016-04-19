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
        public event PropertyChangedEventHandler PropertyChanged;
        ITelnetClient telnetClient;
        volatile Boolean stop;      


        public Model(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.stop = false;
        }


        public void Connect(string ip, int port)
        {
            this.telnetClient.Connect(ip, port);
            this.telnetClient.Start();
        }


        public void Send(string toSend)
        {
            this.telnetClient.Send(toSend);
        }


        public void Disconnect()
        {
            stop = true;
            this.telnetClient.Disconnect();
        }



        public void Start()
        {
            this.telnetClient.Start();
        }



        public void Publish(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
