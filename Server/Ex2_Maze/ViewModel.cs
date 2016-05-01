using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex1_Maze;

namespace Ex2_Maze
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public GeneralMaze<int> playerMaze;
        public IMazeModel model;
        

        public ViewModel(IMazeModel m)
        {
            this.model = m;
            m.PropertyChanged += delegate (object seder, PropertyChangedEventArgs e)
            {
                PublishEvent("VM_" + e.PropertyName);
            };
        }




        public void Command(string s)
        {
            model.Send(s);
        }

        public void MovePlayer(string direction, string sender)
        {
            model.MovePlayer(direction, sender);
        }


        public string VW_Generate
        {
            get { return model.Generate; }
        }




        public List<List<int>> VM_Maze
        {
            get { return model.Maze; }
        }


        public List<List<int>> VM_MyMaze
        {
            get { return model.myMazeList; }
        }

        public List<List<int>> VM_Player2Maze
        {
            get { return model.player2MazeList; }
        }


 


        public void VM_player2Move()
        {
            Console.WriteLine("sadasd");
        }


        public void PublishEvent(string propName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        private decimal _numValue = -1;
        public decimal NumericValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                PublishEvent("NumericValue");
            }
        }


        public void Connect(string IP, string port)
        {
            this.model.Connect(IP, Int32.Parse(port));
        }



        public Boolean VM_Connected
        {
            get { return model.Connected; }
        }           

    }
}
