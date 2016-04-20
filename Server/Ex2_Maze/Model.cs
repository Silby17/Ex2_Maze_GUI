using Ex1_Maze;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Ex2_Maze
{
    public class Model : IMazeModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ITelnetClient telnetClient;
        private Socket server;
        Thread receiverThread;
        private string fromServer;
        private string generate;
        


        public Model(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
        }


        public void Connect(string ip, int port)
        {
            this.telnetClient.Connect(ip, port);
            this.server = telnetClient.GetServer();
        }



        public void Send(string toSend)
        {
            if(toSend[0].Equals('1'))
            {
                this.telnetClient.Send(toSend);
                Generate = telnetClient.Read();
                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //GeneralMaze<int> maze = ser.Deserialize<GeneralMaze<int>>(Generate);
            }
        }

       


        public void Disconnect()
        {
            this.telnetClient.Disconnect();
        }




        public void Publish(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        

        public string Generate
        {
            get { return generate; }
            set { generate = value;
                Publish("Generate"); }
        }

    }
}
