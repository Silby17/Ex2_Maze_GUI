using System.Collections.Generic;
using System.ComponentModel;
using Ex1_Maze;
using System;


namespace Ex2_Maze
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public GeneralMaze<int> playerMaze;
        public IMazeModel model;
        

        /// <summary>
        /// Constructor Method
        /// </summary>
        /// <param name="m">Model</param>
        public ViewModel(IMazeModel m)
        {
            this.model = m;
            m.PropertyChanged += delegate (object seder, PropertyChangedEventArgs e)
            {
                PublishEvent("VM_" + e.PropertyName);
            };
        }


        /// <summary>
        /// This method will send the string to the model</summary>
        /// <param name="s">string to be sent</param>
        public void Command(string s)
        { model.Send(s);}


        /// <summary>
        /// This metyhod will send a connection request
        /// togehter with the params to the model to connect to
        /// the server</summary>
        /// <param name="IP">IP address</param>
        /// <param name="port">Port to connect to</param>
        public void Connect(string IP, string port)
        { this.model.Connect(IP, Int32.Parse(port)); }


        /// <summary>
        /// This method will send the move to the model
        /// and the one who send the move command</summary>
        /// <param name="direction">Move direction</param>
        /// <param name="sender">Player that made the move</param>
        public void MovePlayer(string direction, string sender)
        { model.MovePlayer(direction, sender); }


        /// <summary>
        /// Memebr - Generate
        /// </summary>
        public string VW_Generate
        {
            get { return model.Generate; }
        }


        /// <summary>
        /// Memebr - Solve
        /// </summary>
        public string VW_Solve
        {
            get { return model.Solve; }
        }

        /// <summary>
        /// Memebr - Maze Name
        /// </summary>
        public string VW_MazeName
        {
            get { return model.myGeneralMaze.Name; }
        }


        /// <summary>
        /// Memeber - Maze List
        /// </summary>
        public List<List<int>> VM_Maze
        {
            get { return model.myMazeList; }
        }


        /// <summary>
        /// Memebr - Opponents Maze List
        /// </summary>
        public List<List<int>> VM_Player2Maze
        {
            get { return model.player2MazeList; }
        }


        /// <summary>
        /// Method that will publish events to all subscribers
        /// </summary>
        /// <param name="propName">Message to publish</param>
        public void PublishEvent(string propName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        /// <summary>
        /// This Method will Handle the closing of a window
        /// </summary>
        /// <param name="sender">Window being closed</param>
        /// <param name="e">Event Arguements</param>
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            //If the Multiplayer window is being closed
            if(sender is Multiplayer)
            {
                //Stops Background music
                ((Multiplayer)sender).StopMusic();
                string gameToClose = ((Multiplayer)sender).GameName;
                string closeCommand = "5 " + gameToClose;
                model.Send(closeCommand);
            }
            //The Play window is being closed
            else if(sender is Play)
            {
                ((Play)sender).StopMusic();
            }
        }

                
        /// <summary>
        /// Memeber - Connected Boolean
        /// </summary>
        public Boolean VM_Connected
        { get { return model.Connected; }}           
    }
}