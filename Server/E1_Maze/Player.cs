using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Linq;
using System.IO;
using Ex1_Maze;
using System;

namespace Ex1_Maze
{
    public class Player
    {
        public string Name { get; set;}
        public string MazeName { get; set; }
        public GeneralMaze<int> You { get; set; }
        public GeneralMaze<int> Other { get; set; }

        private Socket clientSocket;
        private GeneralMaze<int> playerMaze;
        private Node<int> currentNode;
        

        public Player() { }
        /// <summary>
        /// Constuctor method that will set the Client</summary>
        /// <param name="client">The Players Socket details</param>
        public Player(Socket client)
        {
            this.clientSocket = client;
        }


        /// <summary>
        /// Sets the maze for the current Player</summary>
        /// <param name="m">maze to be set</param>
        public void SetPlayerMaze(GeneralMaze<int> m)
        {
            this.playerMaze = m;
            this.currentNode = playerMaze.GetStartPoint();
        }


        /// <summary>
        /// Method that will make the necessary move according
        /// to the users input</summary>
        /// <param name="direction">Direction of the move</param>
        public void Move(string direction)
        {
            switch (direction.ToLower())
            {
                case "up":
                    playerMaze.SetCell(currentNode.GetRow() - 1,
                        currentNode.GetCol(), 3);
                    this.currentNode = playerMaze.GetNode(
                        currentNode.GetRow() - 1, currentNode.GetCol());
                    break;

                case "right":
                    playerMaze.SetCell(currentNode.GetRow(),
                        currentNode.GetCol() + 1, 3);
                    this.currentNode = playerMaze.GetNode(
                        currentNode.GetRow(), currentNode.GetCol() + 1);
                    break;

                case "down":
                    playerMaze.SetCell(currentNode.GetRow() + 1,
                        currentNode.GetCol(), 3);
                    this.currentNode = playerMaze.GetNode(
                        currentNode.GetRow() + 1, currentNode.GetCol());
                    break;

                case "left":
                    playerMaze.SetCell(currentNode.GetRow(),
                        currentNode.GetCol() - 1, 3);
                    this.currentNode = playerMaze.GetNode(
                        currentNode.GetRow(), currentNode.GetCol() - 1);
                    break;
            }
        }


        /// <summary>
        /// Returns the socket of the current Player</summary>
        /// <returns>Socket of this player</returns>
        public Socket GetPlayerSocket()
        {
            return this.clientSocket;
        }


        /// <summary>
        /// Returns the Maze of the current Player</summary>
        /// <returns>The players maze</returns>
        public GeneralMaze<int> GetPlayerMaze()
        {
            return this.playerMaze;
        }
    }
}