using System.Collections.Generic;
using System.Net.Sockets;
using Ex1_Maze;
using System;


namespace Server.Options
{
    public class Close : ICommandable
    {
        public event ExecutionDone execDone;
        private Socket clientToReturnTo;
        private string game;
        private Game gameToClose;


        /// <summary>
        /// Constructor Method that will get a list or arguments, the client that 
        /// send the request and the List of Mazes if needed</summary>
        /// <param name="args">List of arguments from client</param>
        /// <param name="client">Socket of the sender</param>
        /// <param name="mazeList">List of mazes</param>
        public void Execute(List<object> args, Socket client, Dictionary<string, GeneralMaze<int>> mazeList)
        {
            this.clientToReturnTo = client;
            this.game = (string)args[1];
            List<Game> games = (List<Game>)args[2];

            foreach (Game g in games)
            {
                if(g.GetGameName() == game)
                {
                    this.gameToClose = g;
                    break;
                }
            }
            PublishEvent();
        }



        /// <summary>
        /// This will publish an event to all its listeners</summary>
        public void PublishEvent()
        {
            if (execDone != null)
            {
                execDone(this, EventArgs.Empty);
            }
        }


        /// <summary>
        /// Returns the Client socket that send the request</summary>
        /// <returns>Clients Socket Details</returns>
        public Socket GetClientSocket()
        { return this.clientToReturnTo; }


        /// <summary>
        /// Returns the game that needs to be closed</summary>
        /// <returns>The game to close</returns>
        public Game GetGameToClose()
        { return this.gameToClose; }
    }
}