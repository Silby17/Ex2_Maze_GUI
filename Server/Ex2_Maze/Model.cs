﻿using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using Ex1_Maze;
using System;


namespace Ex2_Maze
{
    public class Model : IMazeModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ITelnetClient telnetClient;
        JavaScriptSerializer ser;
        public string generate;
        public string solve;
        public string multiplayer;
        private Boolean connected;
        private Player player;
        public List<List<int>> mazeList;
        public List<List<int>> myMazeList { get; set; }
        public List<List<int>> player2MazeList { get; set; }

        public GeneralMaze<int> myGenMaze { get; set; }
        public GeneralMaze<int> singlePlayerSolved { get; set; }
        public GeneralMaze<int> player2GenMaze { get; set; }
        

        public JPosition currentNode { get; set; }
        public JPosition myCurrentNode { get; set; }
        public JPosition plyr2CurrentNode { get; set; }
        public JPosition endNode { get; set; }
        private int WIDTH;
        private int HEIGHT;

        //Single player Members
        public GeneralMaze<int> genMaze { get; set; }
        public List<List<int>> singlePlayerList { get; set; }


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
            this.telnetClient.PropertyChanged += OnEventHandler;
            ser = new JavaScriptSerializer();
        }

        public void OnEventHandler(object sender, PropertyChangedEventArgs args)
        {
            if(sender is TelnetClient)
            {
                if (args.PropertyName.Equals("Player_Moved"))
                {
                    string playerMove = telnetClient.playerMove;
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    Ex1_Maze.Play play = ser.Deserialize<Ex1_Maze.Play>(playerMove);
                    MovePlayer(play.Move, "player2Move");
                    Publish("Player_Moved");
                }
            }
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


        public void StartThread()
        {
            telnetClient.StartThread();
        }

        public void KillThread()
        {
            telnetClient.KillThread();
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
            else if(toSend[0].Equals('2'))
            {
                this.telnetClient.Send(toSend);
                Solve = telnetClient.Read();
            }
            else if(toSend[0].Equals('3'))
            {
                this.telnetClient.Send(toSend);
                Multiplay = telnetClient.Read();
            }
            else if(toSend[0].Equals('4'))
            {
                this.telnetClient.Send(toSend);
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

        public string Solve
        {
            get { return solve; }
            set { solve = value;
                ConvertSolveString();
                Publish("Solve");
            }
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
            get { return mazeList; }
            set { mazeList = value;
                Publish("Maze");
            }
        }



        public void MovePlayer(string direction, string sender)
        {
            if(sender == "play")
            {
                Move(this.mazeList, direction, this.currentNode, this.endNode);
            }
            else if(sender == "myMove")
            {
                Move(this.myMazeList, direction, myCurrentNode, myGenMaze.End);
                telnetClient.Send("4 " + direction);
            }
            else if(sender == "player2Move")
            {
                Move(this.player2MazeList, direction, plyr2CurrentNode, player2GenMaze.End);
            }   
        }


        /// <summary>
        /// This method will move the player in the correct maze</summary>
        /// <param name="maze"></param>
        /// <param name="direction"></param>
        /// <param name="currentNode"></param>
        /// <param name="endNode"></param>
        public void Move(List<List<int>> maze, string direction, JPosition currentNode, JPosition endNode)
        {
            //UP
            if(direction == "up" && currentNode.Row != 0 && maze[currentNode.Row-1][currentNode.Col] != 1)
            {
                maze[currentNode.Row][currentNode.Col] = 0;
                currentNode.Row = currentNode.Row - 1;
                maze[currentNode.Row][currentNode.Col] = 5;
            }
            //DOWN
            else if (direction == "down" && currentNode.Row !=
                this.HEIGHT-1 && maze[currentNode.Row + 1][currentNode.Col] != 1)
            {
                maze[currentNode.Row][currentNode.Col] = 0;
                currentNode.Row = currentNode.Row + 1;
                maze[currentNode.Row][currentNode.Col] = 5;
            }
            //RIGHT
            else if (direction == "right" && currentNode.Col !=
                this.WIDTH - 1 && maze[currentNode.Row][currentNode.Col+1] != 1)
            {
                maze[currentNode.Row][currentNode.Col] = 0;
                currentNode.Col = currentNode.Col + 1;
                maze[currentNode.Row][currentNode.Col] = 5;
            }
            //LEFT
            else if (direction == "left" && currentNode.Col != 0 &&
                maze[currentNode.Row][currentNode.Col-1]!=1)
            {
                maze[currentNode.Row][currentNode.Col] = 0;
                currentNode.Col = currentNode.Col - 1;
                maze[currentNode.Row][currentNode.Col] = 5;
                
            }
            if (currentNode.Row == endNode.Row && currentNode.Col == endNode.Col)
            {
                Publish("Winner");
            }
        }


        /// <summary>
        /// This method will convert the maze data for binding in the gui</summary>
        public void ConvertMultiplayerData()
        {
            this.player = this.ser.Deserialize<Player>(multiplayer);
            //Sets the Mazes of my maze and my opponents maze
            this.myGenMaze = player.You;
            this.player2GenMaze = player.Other;
            myCurrentNode = player.You.Start;
            plyr2CurrentNode = player.Other.Start;
            //Makes a list from Opponents maze
            this.player2MazeList = MakeMazeList(player.Other);
            Publish("Player2Maze");
            //Makes a list from my Maze
            this.myMazeList = MakeMazeList(player.You);
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
                    list[i].Add((int)Char.GetNumericValue(maze.Maze[i * 8 + j]));
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
            this.genMaze = this.ser.Deserialize<GeneralMaze<int>>(Generate);
            this.mazeList = MakeMazeList(this.genMaze);
            this.currentNode = this.genMaze.Start;
            this.endNode = this.genMaze.End;
        }


        /// <summary>
        /// This will convert the received solved Maze into a General Maze
        /// </summary>
        public void ConvertSolveString()
        {
            this.singlePlayerSolved = this.ser.Deserialize<GeneralMaze<int>>(Solve);
            this.singlePlayerList = MakeMazeList(this.singlePlayerSolved);
        }
}
}