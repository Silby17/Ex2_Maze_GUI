using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using Ex1_Maze;
using System;


namespace Ex2_Maze
{
    public class Model : IMazeModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool MultiplayerGameinProgress;
        ITelnetClient telnetClient;
        JavaScriptSerializer ser;
        private int WIDTH;
        private int HEIGHT;
        public string generate;
        public string solve;
        public string multiplayer;
        private Boolean connected;
        private Player player;
        

        //My General Maze and its List version
        public GeneralMaze<int> myGeneralMaze { get; set; }
        public List<List<int>> myMazeList { get; set; }
        public JPosition myCurrentNode { get; set; }

        //Opponents General maze and List version
        public GeneralMaze<int> player2GeneralMaze { get; set; }
        public List<List<int>> player2MazeList { get; set; }
        public JPosition plyr2CurrentNode { get; set; }
        
        public GeneralMaze<int> mySolvedGeneralMaze { get; set; }
        public List<List<int>> mySolvedMazeList { get; set; }
                 
        
        /// <summary>
        /// Constructor method that will get a new instance of
        /// TelnetClient which will be in charge of communication
        /// with the server</summary>
        /// <param name="telnetClient">Communation handler</param>
        public Model(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            //Sets the maze size from the AppConfig file
            this.WIDTH = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Width"]);
            this.HEIGHT = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Bredth"]);
            this.telnetClient.PropertyChanged += OnEventHandler;
            ser = new JavaScriptSerializer();
        }


        /// <summary>
        /// Event Handler that will handle all events that the Model
        /// receives from its Publishers </summary>
        /// <param name="sender">Publisher</param>
        /// <param name="args">Published parameters</param>
        public void OnEventHandler(object sender, PropertyChangedEventArgs args)
        {
            if (sender is TelnetClient)
            {
                //Read the string that the server sent
                string fromServer = telnetClient.fromServer;
                //Checks if the data from the server is a move of the other player
                if(fromServer.Contains("Move"))
                {
                    Ex1_Maze.Play play = ser.Deserialize<Ex1_Maze.Play>(fromServer);
                    MovePlayer(play.Move, "player2Move");
                    Publish("Player_Moved");
                }
                //Else checks if the data is in the form of a general Maze which
                //Is the solved maze received from chosing optioon 2.
                else if(fromServer.Contains("Name") && fromServer.Contains("Maze"))
                {
                    this.mySolvedGeneralMaze = ser.Deserialize<GeneralMaze<int>>(fromServer);
                    this.mySolvedMazeList = MakeMazeList(mySolvedGeneralMaze);
                    JPosition ans = GetBestMove(mySolvedMazeList, myCurrentNode);
                    this.myMazeList[ans.Row][ans.Col] = 4;
                    Publish("SuggestionReceived");
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


        /// <summary>
        /// Starts the running of the Thread that will
        /// constantly be waiting for input from the server
        /// </summary>
        public void StartThread()
        { telnetClient.StartThread();}


        /// <summary>
        /// Kills the thread that is constantly listening 
        /// for input from the server</summary>
        public void KillThread()
        { telnetClient.KillThread(); }


       /// <summary>
       /// This method will pass on command to the TelnetClient
       /// to send msg to the Server</summary>
       /// <param name="toSend">message to send</param>
        public void Send(string toSend)
        {
            //1 - Generate new Maze
            if(toSend[0].Equals('1'))
            {
                //Send request
                this.telnetClient.Send(toSend);
                //Get the generated Maze from Server
                Generate = telnetClient.Read();                
            }
            //2 - Solve Maze
            else if(toSend[0].Equals('2'))
            {
                //Send the request to the server
                this.telnetClient.Send(toSend);
                if(!MultiplayerGameinProgress)
                {
                    //If we in single player mode then get the
                    //solved maze immediatly
                    Solve = telnetClient.Read();
                }   
            }
            //3 - Multiplayer
            else if (toSend[0].Equals('3'))
            {
                this.telnetClient.Send(toSend);
                Multiplay = telnetClient.Read();
            }
            //4 - Play
            else if(toSend[0].Equals('4'))
            {
                this.telnetClient.Send(toSend);
            }
            //5 - Close the Game
            else if(toSend[0].Equals('5'))
            {
                this.telnetClient.Send(toSend);
            }
        }
       

        /// <summary>
        /// Send request to TelnetClient to disconnect from the server
        /// </summary>
        public void Disconnect()
        { this.telnetClient.Disconnect(); }

        
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


        /// <summary>
        /// Solved maze in string Form
        /// </summary>
        public string Solve
        {
            get { return solve; }
            set { solve = value;
                ConvertSolveString();
                //Gets the node where the next move should be
                JPosition ans = GetBestMove(this.mySolvedMazeList, this.myCurrentNode);
                //Sets the suggestion position to 4
                myMazeList[ans.Row][ans.Col] = 4;
            }
        }


        /// <summary>
        /// Multiplay Property</summary>
        public string Multiplay
        {
            get { return multiplayer; }
            set { multiplayer = value;
                ConvertMultiplayerData();
                MultiplayerGameinProgress = true;
                Publish("Multiplayer");
            }
        }


        /// <summary>
        /// Connected Boolean</summary>
        public Boolean Connected
        {
            get { return connected; }
            set { connected = value;
                Publish("Connected");
            }
        }

        /// <summary>
        /// Holds the Maze
        /// </summary>
        public List<List<int>> Maze
        {
            get { return myMazeList; }
            set { myMazeList = value;
                Publish("Maze");
            }
        }
        

        /// <summary>
        /// This method will send the correct details to move the player.
        /// </summary>
        /// <param name="direction">Which direction to move in</param>
        /// <param name="sender">Who wants to make the move</param>
        public void MovePlayer(string direction, string sender)
        {
            //Single PLayer sending the move request
            if(sender == "play")
            {
                Move(myMazeList, direction, myCurrentNode,
                    myGeneralMaze.End, "myMove");
            }
            //MyMove in Multiplayer Game
            else if(sender == "myMove")
            {
                Move(this.myMazeList, direction, myCurrentNode,
                    this.myGeneralMaze.End, "myMove");
                telnetClient.Send("4 " + direction);
            }
            //Move of the opponent
            else if(sender == "player2Move")
            {
                Move(this.player2MazeList, direction, this.plyr2CurrentNode,
                    player2GeneralMaze.End, "otherMovie");
            }   
        }

                
        /// <summary>
        /// This method will move the player in the correct maze</summary>
        /// <param name="maze"></param>
        /// <param name="direction"></param>
        /// <param name="currentNode"></param>
        /// <param name="endNode"></param>
        public void Move(List<List<int>> maze, string direction, JPosition currentNode, JPosition endNode, string sender)
        {
            //UP
            if(direction == "up" && currentNode.Row != 0 &&
                maze[currentNode.Row-1][currentNode.Col] != 1)
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
            //Checks if the player has reached their End Point
            if (currentNode.Row == endNode.Row && currentNode.Col == endNode.Col)
            {
                //If the player is my Move
                if(sender == "myMove")
                {
                    Publish("Winner");
                }
                //Or if the player that reached the goal is the opponent
                else { Publish("P2_Winner"); }
            }
        }


        /// <summary>
        /// This method will convert the maze data for binding in the gui</summary>
        public void ConvertMultiplayerData()
        {
            this.player = this.ser.Deserialize<Player>(multiplayer);
            //Sets the Mazes of my maze and my opponents maze
            this.myGeneralMaze = player.You;
            this.player2GeneralMaze = player.Other;
            myCurrentNode = player.You.Start;
            plyr2CurrentNode = player.Other.Start;
            //Makes a list from my Maze
            this.myMazeList = MakeMazeList(this.myGeneralMaze);
            Publish("MyMaze");
            //Makes a list from Opponents maze
            this.player2MazeList = MakeMazeList(this.player2GeneralMaze);
            Publish("Player2Maze");
        }


        /// <summary>
        /// This method converts a generalMaze into a 2D List
        /// </summary>
        /// <param name="maze">maze to convert</param>
        /// <returns>2D List</returns>
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
            this.myGeneralMaze = this.ser.Deserialize<GeneralMaze<int>>(Generate);
            this.myMazeList = MakeMazeList(this.myGeneralMaze);
            this.myCurrentNode = this.myGeneralMaze.Start;
        }


        /// <summary>
        /// This will convert the received solved Maze into a General Maze
        /// </summary>
        public void ConvertSolveString()
        {
            this.mySolvedGeneralMaze = this.ser.Deserialize<GeneralMaze<int>>(Solve);
            this.mySolvedMazeList = MakeMazeList(this.mySolvedGeneralMaze);
        }



        /// <summary>
        /// Returns the best move according to algo.
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="direction"></param>
        /// <param name="currentNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public JPosition GetBestMove(List<List<int>> maze, JPosition currentNode)
        {
            JPosition ans = null;
            //the longest disctance that could be
            double minDis = Math.Pow(HEIGHT * HEIGHT + WIDTH * WIDTH, 0.5);
            for (int i = 0; i < this.WIDTH; i++)
            {
                for (int j = 0; j < this.HEIGHT; j++)
                {   //check the min distance to the path
                    if (2 == maze[i][j])
                    {
                        //compute distance to "2"
                        double DisI = Math.Pow(i - currentNode.Row, 2);
                        double DisJ = Math.Pow(j - currentNode.Col, 2);
                        double Dis = Math.Pow(DisI + DisJ, 0.5);
                        //check if dis is lower than minDis
                        if (Dis < minDis)
                        {
                            minDis = Dis;
                            //Answer of the Closest Node
                            ans = new JPosition(i, j);
                        }
                    }
                }
            }
            return ans;
        }
    }
}