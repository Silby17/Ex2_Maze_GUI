using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2_Maze
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IMazeModel model;

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


        public string VW_Generate
        {
            get { return model.Generate; }
        }


        public void PublishEvent(string propName)
        {

        }

        public void Connect(string IP, string port)
        {
            this.model.Connect(IP, Int32.Parse(port));
        }
    }
}
