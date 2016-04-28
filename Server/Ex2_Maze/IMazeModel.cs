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

        Boolean Connected { get; set; }
        string Generate { get; set; }
        List<List<int>> Maze { get; set; }
        GeneralMaze<int> genMaze { get; set; }
        List<List<int>> myMazeList { get; set; }
        List<List<int>> player2MazeList { get; set; }

    }
}
