using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2_Maze
{
    public interface IMazeModel : INotifyPropertyChanged
    {
        void Connect(string ip, int port);
        void Disconnect();
        void Send(string str);

        Boolean Connected { get; set; }
        string Generate { get; set; }
        List<List<int>> Maze { get; set; }
    }
}
