using System.Web.Script.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System;
using System.Net.Sockets;

namespace Server
{
    public class Presenter : IPresenter
    {
        private IView view; //publisher
        private IModel model; //publisher

        /// <summary>
        /// Constructor Method</summary>
        /// <param Name="v">The view for the Presenter</param>
        /// <param Name="m">The Model for the Presenter</param>
        public Presenter(IView v, IModel m)
        {
            this.view = v;
            this.model = m;
            
            //Subscribe to events from the Model
            model.newModelChange += this.OnEventHandler;

            //Subscribe to events from view
            view.newInput += this.OnEventHandler;
        }

       


        /// <summary>
        /// This function deals with and receives any event that is sent</summary>
        /// <param Name="source">Object from the Publisher</param>
        /// <param Name="e">Any event Arguments</param>
        public void OnEventHandler(object source, EventArgs e)
        {
            if(source is IView)
            {
                HandleViewEvent(source);
            }
            else if(source is IModel)
            {
                HandleModelEvent(source);
            }
        }


        /// <summary>
        /// This Function will get the data that the view wants to 
        /// send to the Presenter </summary>
        public void HandleViewEvent(object src)
        {
            IView v = (IView)src;
            //Gets the new string input from the Client
            string newCommand = v.GetStringInput();
            Socket client = v.GetClient();
            
            //Splits to a list of strings
            List<string> commandList = newCommand.Split(' ').ToList();

            //Splits to a list of objects for passing to thread pool
            List<object> ol = commandList.ConvertAll(s => (object)s);
            
            this.model.ExecuteCommandalbe(ol, client);
        }

        

        /// <summary>
        /// This function will get the message from the Model to
        /// send to the cliet in the JSON format</summary>
        public void HandleModelEvent(object src)
        {
            throw new NotImplementedException();
        }
    }
}