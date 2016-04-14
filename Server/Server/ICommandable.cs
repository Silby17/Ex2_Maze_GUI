using System.Collections.Generic;
using System.Net.Sockets;
using Ex1_Maze;
using System;


namespace Server
{
    public delegate void ExecutionDone(Object source, EventArgs e);

    public interface ICommandable
    {
        event ExecutionDone execDone;
        void Execute(List<object> args, Socket client, Dictionary<string, GeneralMaze<int>> mazeList);
        Socket GetClientSocket();
        void PublishEvent();
    }
}