using Ex1_Maze;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Ex2_Maze
{
    public class Model : IMazeModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ITelnetClient telnetClient;
        public string generate;
        public string multiplayer;
        private Boolean connected;
        private Player player;
        public List<List<int>> maze;
        public List<List<int>> myMaze { get; set; }
        public List<List<int>> player2Maze { get; set; }
        public GeneralMaze<int> myGenMaze { get; set; }
        public GeneralMaze<int> player2GenMaze { get; set; }
        public GeneralMaze<int> genMaze { get; set; }
        public JPosition currentNode { get; set; }
        private int WIDTH;
        private int HEIGHT;
        
        

        /// <summary>
        /// Constructor method that will get a new instance of
        /// TelnetClient which will be in charge of communication
        /// with the server</summary>
        /// <param name="telnetClient">Communation handler</param>
        public Model(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.WIDTH = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Width"]);
            this.HEIGHT = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Bredth"]);
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
            else if(toSend[0].Equals('3'))
            {
                this.telnetClient.Send(toSend);
                Multiplay = telnetClient.Read();
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
                ConvertGenerate();
                Publish("Generate"); }
        }

        public string Multiplay
        {
            get { return multiplayer; }
            set { multiplayer = value;
                ConvertMultiplayerData();
                Publish("Multiplayer");
            }
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
            set { maze = value;
                Publish("Maze");
            }
        }


        public void Move(string direction)
        {
            if(direction == "up")
            {
                currentNode.Row = currentNode.Row - 1;
            }
            maze[currentNode.Row][currentNode.Col] = 5;
        }



        public void ConvertMultiplayerData()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            this.player = ser.Deserialize<Player>(multiplayer);
            this.player2Maze = MakeMazeList(player.Other);
            Publish("Player2Maze");
            this.myMaze = MakeMazeList(player.You);
            Publish("MyMaze");
        }



        public List<List<int>> MakeMazeList(GeneralMaze<int> maze)
        {
            List<List<int>> list = new List<List<int>>();
            for (int i = 0; i < this.WIDTH; i++)
            {
                list.Add(new List<int>());
                for (int j = 0; j < this.HEIGHT; j++)
                {
                    list[i].Add((int)Char.GetNumericValue(maze.Maze[i * 5 + j]));
                }
            }
            int sCol = maze.Start.Col;
            int sRow = maze.Start.Row;
            list[sRow][sCol] = 5;

            int eRow = maze.End.Row;
            int eCol = maze.End.Col;
            list[eRow][eCol] = 9;
            return list;
        }


        /// <summary>
        /// This method takes the JSON from the Generation action
        /// deserializes it into GeneralMaze and then converts the MazeString
        /// into a list which is set to the databinding in the grid </summary>
        public void ConvertGenerate()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            this.genMaze = ser.Deserialize<GeneralMaze<int>>(Generate);
            this.maze = MakeMazeList(this.genMaze);
        }
    }
}