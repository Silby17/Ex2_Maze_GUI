using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class View : IView
    {
        private string commandToSend;
        public event NewViewChangeEvent newInput;
        private IPresenter presenter;
        private IModel model;
        private Socket client;
        

        /// <summary>
        /// Simple Constructor Method/// </summary>
        public View(Socket client, IModel model)
        {
            this.client = client;
            this.model = model;
            this.presenter = new Presenter(this, this.model);
        }
    


        /// <summary>
        /// Mehtod that will be used to start the handleing of the Clients
        /// it will also send a welcome msg to the client</summary>
        public void handle()
        {
            byte[] data = new byte[1024];
            string wlc = "Welcome";
            data = Encoding.ASCII.GetBytes(wlc);
            //Server sends welcome msg to Client
            client.Send(data, data.Length, SocketFlags.None);
            Thread recThread = new Thread(ReceiveThread);
            recThread.Start();
        }


        /// <summary>
        /// This method with be run by a thread and will be in charge
        /// of receiving data</summary>
        public void ReceiveThread()
        {
            byte[] data = new byte[1024];
            while (true)
            {
                try
                {
                    data = new byte[1024];
                    int recv = client.Receive(data);
                    if (recv == 0) break;
                    string str = Encoding.ASCII.GetString(data, 0, recv);
                    if (str.Equals("exit")) break;
                    OnNewInput(str, client);
                }
                catch
                {
                    client.Close();
                }   
            }
        }


        /// <summary>
        /// This function will run when there is a new input
        /// from the client that needs to be sent to the server
        /// </summary>
        /// <param Name="str">The input received</param>
        public void OnNewInput(string str, Socket client)
        {
            commandToSend = str;
            PublishEvent();
        }


        /// <summary>
        /// Sends out an event signal to all subscribers</summary>
        public void PublishEvent()
        {
            if (newInput != null)
            {
                newInput(this, EventArgs.Empty);
            }
        }


        /// <summary>
        /// Returns the string input to any request</summary>
        /// <returns>The input from the Client</returns>
        public string GetStringInput()
        {
            return this.commandToSend;
        }


        /// <summary>
        /// Returns the client details in socket format</summary>
        /// <returns>The current Socket info</returns>
        public Socket GetClient()
        { return this.client; }
    }
}