using System.Web.Script.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Linq;
using System.IO;
using Ex1_Maze;
using System;


namespace Server.Options
{
    public class Generate : ICommandable
    {
        public event ExecutionDone execDone;
        private string JSONMaze;
        private GeneralMaze<int> gMa;
        private Socket clientToReturnTo;
        private string mazeName;

           
        /// <summary>
        /// Constructor Method that will get a list or arguments, the client that 
        /// send the request and the List of Mazes if needed</summary>
        /// <param name="args">List of arguments from client</param>
        /// <param name="client">Socket of the sender</param>
        /// <param name="mazeList">List of mazes</param>
        public void Execute(List<object> args, Socket client, Dictionary<string, GeneralMaze<int>> mazeList)
        {
            this.clientToReturnTo = client;
            List<string> strings = args.Select(s => (string)s).ToList();
            string name = strings[1];
            int type = Int32.Parse(strings[2]);
            this.mazeName = name;
            if(mazeList.ContainsKey(name))
            {
                GeneralMaze<int> temp;
                mazeList.TryGetValue(name, out temp);
                this.gMa = temp;
                JavaScriptSerializer ser = new JavaScriptSerializer();
                string jsonMaze = ser.Serialize(temp);
                jsonMaze = JToken.Parse(jsonMaze).ToString();
                this.JSONMaze = jsonMaze;
                PublishEvent();
            }
            else
            {
                GenerateMaze(name, type);
            }
        }


        /// <summary>
        /// Generate a new maze </summary>
        /// <param Name="name">Name of the the new maze</param>
        /// <param Name="type">The type of the new maze</param>
        public void GenerateMaze(string name, int type)
        {
            int height = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["HEIGHT"]);
            int width = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["WIDTH"]);
            _2DMaze<int> maze = new _2DMaze<int>(height, width);
            GeneralMaze<int> cMaze = new GeneralMaze<int>(maze);
            cMaze.Generate(name, type);
            this.gMa = cMaze;

            JavaScriptSerializer ser = new JavaScriptSerializer();
            string jsonMaze = ser.Serialize(cMaze);
            jsonMaze = JToken.Parse(jsonMaze).ToString();
            File.WriteAllText(name+".json", jsonMaze);

            this.JSONMaze = jsonMaze;
            PublishEvent();
        }


        /// <summary>
        /// This method will publish an event that the maze
        /// generation is completed</summary>
        public void PublishEvent()
        {
            if(execDone != null) { execDone(this, EventArgs.Empty); }
        }

        public string GetJSON()
        { return this.JSONMaze;}

        /// <summary>
        /// Returns the General Maze</summary>
        /// <returns>The Maze that was generated</returns>
        public GeneralMaze<int> GetGmaze() { return gMa; }


        /// <summary>
        /// Returns the Client socket that send the request</summary>
        /// <returns>Clients Socket Details</returns>
        public Socket GetClientSocket()
        { return this.clientToReturnTo; }


        /// <summary>
        /// Returns the Name of the maze</summary>
        /// <returns>Gets the name of the Maze</returns>
        public string GetMazeName()
        { return this.mazeName; }
    }
}