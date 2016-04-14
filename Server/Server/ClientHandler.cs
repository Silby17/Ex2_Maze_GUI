using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class ClientHandler
    {
        /**
        private Socket client;
        private IPresenter chPresenter;
        private IView chView;
        private int ID;


        /// <summary>
        /// Constructor method</summary>
        /// <param Name="socket">The socket of client</param>
        /// <param Name="model">The main Model</param>
        public ClientHandler(Socket socket, IModel model, int id)
        {
            this.client = socket;
            this.ID = id;
            chView = new ClientView();
            chPresenter = new ClientPresenter(chView, model);
        }

        
        /// <summary>
        /// The function that the server will use to handle
        /// the client</summary>
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
                data = new byte[1024];
                int recv = client.Receive(data);
                if (recv == 0) break;
                string str = Encoding.ASCII.GetString(data, 0, recv);
                if (str.Equals("exit")) break;
                handleRequest(str);
            }
            client.Close();
        }


        /// <summary>
        /// This method will be run on a seperate thread
        /// and it will be in charge of sending data to the Client</summary>
        /// <param name="str">The string to send to Client</param>
        public void Send(string str)
        {
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(str);
            //Server sends welcome msg to Client
            client.Send(data, data.Length, SocketFlags.None);
        }


        /// <summary>
        /// Handles the request from the client</summary>
        /// <param Name="s">The command from the client</param>
        public void handleRequest(string s)
        {
            chView.NewInput(s);
        }
    }
    **/
    }
}