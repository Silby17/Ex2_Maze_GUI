using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex1_Maze;
namespace Ex2_Maze
{
    public interface IMazeModel : INotifyPropertyChanged
    {
        void Connect(string ip, int port);
        void Disconnect();
        void Send(string str);
        void MovePlayer(string direction, string sender);
        void StartThread();
        void KillThread();

        Boolean Connected { get; set; }
        string Generate { get; set; }
        string Solve { get; set; }

        GeneralMaze<int> myGeneralMaze { get; set; }
        GeneralMaze<int> player2GeneralMaze { get; set; }
        List<List<int>> myMazeList { get; set; }
        List<List<int>> player2MazeList { get; set; }

    }
}
