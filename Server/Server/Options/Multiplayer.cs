using System.Web.Script.Serialization;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using Ex1_Maze;
using Newtonsoft.Json.Linq;

namespace Server.Options
{
    public class Multiplayer : ICommandable
    {
        public event ExecutionDone execDone;
        private Socket clientToReturnTo;
        private Dictionary<string, GeneralMaze<int>> mazeList;
        private int HIEGHT, WIDTH;
        private List<Game> games;
        JavaScriptSerializer ser;


        /// <summary>
        /// Constructor Method that will get a list or arguments, the client that 
        /// send the request and the List of Mazes if needed</summary>
        /// <param name="args">List of arguments from client</param>
        /// <param name="client">Socket of the sender</param>
        /// <param name="mazeList">List of mazes</param>
        public void Execute(List<object> args, Socket client, Dictionary<string, GeneralMaze<int>> mazeList)
        {
            this.ser = new JavaScriptSerializer();
            this.mazeList = mazeList;
            SetSize();           
            
            //Creates a new Player
            Player player = new Player(client);

            //Gets the name of the Game
            string gameName = (string)args[1];

            player.Name = gameName;
            //Converts the List of games from type object
            this.games = (List<Game>)args[2];

            //If the Games List is not empty
            if(games.Count != 0)
            {
                foreach(Game g in games)
                {
                    if(g.GetGameName() == gameName)
                    {
                        //The Game already Exists in the list of games

                        //Adds new player to the game
                        g.AssignPlayerToGame(player);
                        g.CreateSecondMaze();
                        player.SetPlayerMaze(g.GetPlayer2Maze());
                        player.MazeName = player.GetPlayerMaze().Name;
                        string you = ser.Serialize(player.GetPlayerMaze());
                        player.You = JToken.Parse(you).ToString();
                        string other = ser.Serialize(games[0].playersList[0].GetPlayerMaze());
                        player.Other = JToken.Parse(other).ToString();
                        games[0].playersList[0].Other = you;
                        g.SetPlayers();
                        PublishEvent();                        
                    }
                    else
                    //The current Game doesnt Exist
                    {CreateNewGame(gameName, player); }
                }
            }
            //Create a new Game if the game doesnt Exist
            else
            { CreateNewGame(gameName, player);}
        }


        /// <summary>
        /// THis method will create a new Game if the game doesn't
        /// already exist </summary>
        /// <param name="gameName">The Name of the game</param>
        /// <param name="player">The first PLayer</param>
        public void CreateNewGame(string gameName, Player player)
        {
            GeneralMaze<int> newMaze = CreateMultiMaze(gameName);
            this.mazeList.Add(newMaze.Name, newMaze);
            Game newGame = new Game(newMaze, gameName);
            newGame.AssignPlayerToGame(player);
            games.Add(newGame);
            player.SetPlayerMaze(newMaze);
            player.MazeName = player.GetPlayerMaze().Name;
            string you = ser.Serialize(player.GetPlayerMaze());
            you = JToken.Parse(you).ToString();
            player.You = you;
        }


        /// <summary>
        /// This will publish an event to all its listeners</summary>
        public void PublishEvent()
        {
            if (execDone != null) {execDone(this, EventArgs.Empty);}
        }


        /// <summary>
        /// Returns the Client socket that send the request</summary>
        /// <returns>Clients Socket Details</returns>
        public Socket GetClientSocket()
        { return this.clientToReturnTo; }


        /// <summary>
        /// This method will create a new maze for the multiplayer mode
        /// </summary>
        /// <param name="name">Name of the Maze</param>
        /// <returns>A new general Maze</returns>
        public GeneralMaze<int> CreateMultiMaze(string name)
        {
            _2DMaze<int> newMaze = new _2DMaze<int>(HIEGHT, WIDTH);
            GeneralMaze<int> newGM = new GeneralMaze<int>(newMaze);
            Random rand = new Random();
            int type = rand.Next(0, 2);
            string mazeName = name + "_1" + " " + type;
            newGM.Generate(mazeName, type);
            return newGM;
        }


        /// <summary>
        /// Sets the sizes of the Maze from the config file</summary>
        public void SetSize()
        {
            this.HIEGHT = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["HEIGHT"]);
            this.WIDTH = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["WIDTH"]);
        }


        /// <summary>
        /// Returns the list of games </summary>
        /// <returns></returns>
        public List<Game> GetGameList()
        { return this.games; }
    }
}