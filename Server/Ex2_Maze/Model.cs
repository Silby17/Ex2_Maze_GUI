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
        private string generate;
        private string move;
        

        /// <summary>
        /// Constructor method that will get a new instance of
        /// TelnetClient which will be in charge of communication
        /// with the server</summary>
        /// <param name="telnetClient">Communation handler</param>
        public Model(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
        }



        /// <summary>
        /// This will send params to the TelnetClinet 
        /// inorder to connect to the server </summary>
        /// <param name="ip">ip to connect to</param>
        /// <param name="port">Port to connect to</param>
        public void Connect(string ip, int port)
        {
            this.telnetClient.Connect(ip, port);
        }



       /// <summary>
       /// This method will pass on command to the TelnetClient
       /// to send msg to the Server</summary>
       /// <param name="toSend">message to send</param>
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
       

        /// <summary>
        /// Send request to TelnetClient to disconnect from the server
        /// </summary>
        public void Disconnect()
        {
            this.telnetClient.Disconnect();
        }



        /// <summary>
        /// Publish event change to all the subscribers </summary>
        /// <param name="propName">property name that was changed</param>
        public void Publish(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        
        /// <summary>
        /// Generate property </summary>
        public string Generate
        {
            get { return generate; }
            set { generate = value;
                Publish("Generate"); }
        }
    }
}