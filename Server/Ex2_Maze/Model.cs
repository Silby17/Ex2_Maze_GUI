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
        public string generate;
        private Boolean connected;
        public List<List<int>> maze;
        public GeneralMaze<int> genMaze { get; set; }
        public JPosition currentNode { get; set; }
        
        

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
            this.Connected = telnetClient.Connected;
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
                ConvertStringToMaze();
                Publish("Generate"); }
        }



        public Boolean Connected
        {
            get { return connected; }
            set { connected = value;
                Publish("Connected");
            }
        }


        public List<List<int>> Maze
        {
            get { return maze; }
            set { maze = value; }
        }


        public void Move(string direction)
        {
            if(direction == "up")
            {
                currentNode.Row = currentNode.Row - 1;
            }
            maze[currentNode.Row][currentNode.Col] = 5;
            Publish("Maze");
        }

        /// <summary>
        /// This method takes the JSON from the Generation action
        /// deserializes it into GeneralMaze and then converts the MazeString
        /// into a list which is set to the databinding in the grid </summary>
        public void ConvertStringToMaze()
        {
            this.maze = new List<List<int>>();
            JavaScriptSerializer ser = new JavaScriptSerializer();
            this.genMaze = ser.Deserialize<GeneralMaze<int>>(Generate);
            this.currentNode = genMaze.Start;
            for(int i = 0; i < 5; i++)
            {
                Maze.Add(new List<int>());
                for (int j = 0; j < 5; j++)
                {
                    this.maze[i].Add((int)Char.GetNumericValue(genMaze.Maze[i * 5 + j]));
                }
            }
            int sCol = this.currentNode.Col;
            int sRow = this.currentNode.Row;
            maze[sRow][sCol] = 5;

            int eRow = this.genMaze.End.Row;
            int eCol = this.genMaze.End.Col;
            maze[eRow][eCol] = 9;
        }
    }
}