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
            m.PropertyChanged += delegate (Object seder, PropertyChangedEventArgs e)
            {
                PublishEvent("VM_" + e.PropertyName);
            };
        }


        public void PublishEvent(string propName)
        {

        }

        
    }
}
