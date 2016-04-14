using System.Web.Script.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Linq;
using Ex1_Maze;
using System;


namespace Server.Options
{
    public class Solve : ICommandable
    {
        public event ExecutionDone execDone;
        private Socket clientToReturnTo;
        private string JSONMaze;


        /// <summary>
        /// Constructor Method that will get a list or arguments,
        /// the client that send the request and the
        /// List of Mazes if needed</summary>
        /// <param name="args">List of arguments from client</param>
        /// <param name="client">Socket of the sender</param>
        /// <param name="mazeList">List of mazes</param>
        public void Execute(List<object> args, Socket client, 
            Dictionary<string, GeneralMaze<int>> mazeList)
        {
            this.clientToReturnTo = client;
            List<string> strParams = args.Select(s => (string)s).ToList();
            string mazeName = strParams[1];
            int type = Int32.Parse(strParams[2]);
            if (mazeList.ContainsKey(mazeName))
            {
                mazeList[mazeName].Solve(type);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                string jsonMaze = ser.Serialize(mazeList[mazeName]);
                this.JSONMaze = JToken.Parse(jsonMaze).ToString();
                PublishEvent();
            }            
        }


        /// <summary>
        /// This will publish an event to all its listeners</summary>
        public void PublishEvent()
        {
            if(execDone != null){ execDone(this, EventArgs.Empty);}
        }


        /// <summary>
        /// Returns the Client socket that send the request</summary>
        /// <returns>Clients Socket Details</returns>
        public Socket GetClientSocket()
        { return this.clientToReturnTo; }


        /// <summary>
        /// This will return the JSON format of the Solved maze</summary>
        /// <returns>JSON string</returns>
        public string GetJSON()
        { return this.JSONMaze; }
    }
}