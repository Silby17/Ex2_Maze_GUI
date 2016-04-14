using Ex1_Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_Maze
{
    public class Game
    {
        public List<Player> playersList;
        public List<GeneralMaze<int>> mazeList;
        private string GameName;
        public Player player1{get; set;}
        public Player player2 { get; set; }
        private GeneralMaze<int> mazePlayer1;
        private GeneralMaze<int> mazePlayer2;


        /// <summary>
        /// Constructor method that gets a maze and the name of the Game</summary>
        /// <param name="m">The new game Maze</param>
        /// <param name="name">The Name of the Game</param>
        public Game(GeneralMaze<int> m, string name)
        {
            this.mazePlayer1 = m;
            this.mazeList = new List<GeneralMaze<int>>();
            this.playersList = new List<Player>();
            this.GameName = name;
            this.mazeList.Add(m);
        }


        /// <summary>
        /// Creates a second maze for the Second player
        /// that has joined the game </summary>
        public void CreateSecondMaze()
        {
            Node<int>[,] grid = mazePlayer1.GetGrid();
            Node<int> start = mazePlayer1.GetStartPoint();
            Node<int> end = mazePlayer1.GetEndPoint();

            _2DMaze<int> twoDMaze = new _2DMaze<int>();
            twoDMaze.height = mazePlayer1.GetHeight();
            twoDMaze.width = mazePlayer1.GetWidth();
            twoDMaze.CopyGrid(grid);
            
            //Switches around the starting and ending cell in the Second Maze
            twoDMaze.SetStartingCell(end.GetRow(), end.GetCol());
            twoDMaze.SetEndingCell(start.GetRow(), start.GetCol());
            this.mazePlayer2 = new GeneralMaze<int>(twoDMaze);
            this.mazePlayer2.MakeMazeString();
            this.mazePlayer2.UpdateMembers();
            this.mazePlayer2.Name = this.mazePlayer1.Name + "_2";
        }


        /// <summary>
        /// Returns the name of the Current Game</summary>
        /// <returns>The name of the game</returns>
        public string GetGameName()
        { return this.GameName;}


        /// <summary>
        /// Adds player to the current Game</summary>
        /// <param name="p">Player to be added</param>
        public void AssignPlayerToGame(Player player)
        { this.playersList.Add(player); }


        /// <summary>
        /// Method that returns the list of Players</summary>
        /// <returns></returns>
        public List<Player> GetPlayersList()
        { return this.playersList; }

        
        /// <summary>
        /// Sets the maze for the second player </summary>
        /// <param name="maze">MAze to be set</param>
        public void SetMazePlayer2(GeneralMaze<int> maze)
        { this.mazePlayer2 = maze; }


        /// <summary>
        /// This method will return the player that is playing
        /// against the current Player</summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public Player GetOtherPlayer(Player current)
        {
            if(current == player1)
            {
                return player2;
            }
            else
            {
                return player1;
            }
        }


        /// <summary>
        /// Returns the maze of player1</summary>
        /// <returns>Player 1 maze</returns>
        public GeneralMaze<int> GetPlayer1Maze()
        { return this.mazePlayer1; }


        /// <summary>
        /// Returns the Maze of the second player</summary>
        /// <returns></returns>
        public GeneralMaze<int> GetPlayer2Maze()
        { return this.mazePlayer2; }


        /// <summary>
        /// Sets the players in the Game</summary>
        public void SetPlayers()
        {
            this.player1 = playersList[0];
            this.player2 = playersList[1];
        }
    }
}
