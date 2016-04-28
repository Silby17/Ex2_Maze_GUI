using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Ex1_Maze;

namespace Server.Options
{
    public class Play : ICommandable
    {
        public string Name { get; set; }
        public string Move { get; set; }

        public event ExecutionDone execDone;
        private Socket clientToReturnTo;
        private List<Game> listOfGames;
        private Socket client;
        private Player playerToReturnTo;
        private Game currentGame;

        public Play() { }
        /// <summary>
        /// Constructor Method that will get a list or arguments, the client that 
        /// send the request and the List of Mazes if needed</summary>
        /// <param name="args">List of arguments from client</param>
        /// <param name="client">Socket of the sender</param>
        /// <param name="mazeList">List of mazes</param>
        public void Execute(List<object> args, Socket client, Dictionary<string, GeneralMaze<int>> mazeList)
        {
            this.client = client;
            string move = (string)args[1];
            this.Move = move;
            this.listOfGames = (List<Game>)args[2];
            this.currentGame = listOfGames[0];
            this.Name = currentGame.GetGameName();
            List<Player> listofPlayers = this.listOfGames[0].GetPlayersList();

            foreach (Player p in listofPlayers)
            {
                if(p.GetPlayerSocket() == client)
                {
                    p.Move(move);
                    this.playerToReturnTo = listOfGames[0].GetOtherPlayer(p);
                    this.clientToReturnTo = playerToReturnTo.GetPlayerSocket();
                    PublishEvent();
                }
            }           
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
        public Socket GetSocketToReturnTo()
        { return this.clientToReturnTo; }


        /// <summary>
        /// Returns the clients Socket</summary>
        /// <returns></returns>
        public Socket GetClientSocket()
        {
            return null;
        }
    }
}