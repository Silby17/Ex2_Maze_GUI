﻿using System;
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


        public List<List<int>> VM_Maze
        {
            get { return model.Maze; }
        }


        public void PublishEvent(string propName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
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
