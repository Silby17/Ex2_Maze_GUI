using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System;
using System.ComponentModel;

namespace Ex2_Maze
{
    public class TelnetClient : ITelnetClient
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int PORT;
        private IPAddress IP;
        private Socket server;
        private IPEndPoint ipep;
        public string generated { get; set; }
        public Boolean Connected { get; set; }
        public string playerMove { get; set; }
        private Thread rece;
        private bool runThread;
        


        public TelnetClient()
        {
            this.Connected = false;
        }


        /// <summary>
        /// Connects to the server</summary>
        /// <param name="ip">IP address to connect to</param>
        /// <param name="port">the Port to connect to</param>
        public void Connect(string ip, int port)
        {
            this.PORT = port;
            this.IP = IPAddress.Parse(ip);
            this.ipep = new IPEndPoint(IP, PORT);
            this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Try to connect to the server
            try
            {
                server.Connect(ipep);
                Console.WriteLine("Connected to Server");
                Connected = true;
                Read();
            }
            catch (SocketException e) { Console.WriteLine("Unable to connect to server." + e.ToString()); }

            
        }

        public void Publish(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        public void StartThread()
        {
            runThread = true;
            this.rece = new Thread(ReceiveThread);
            rece.Start();
        }

        public void KillThread()
        {
            runThread = false;
        }

        /// <summary>
        /// This will send data to the Server </summary>
        /// <param name="command">The command to send to server</param>
        public void Send(string command)
        {
            server.Send(Encoding.ASCII.GetBytes(command));
        }


        /// <summary>
        /// Method that will read any expected data from the Server</summary>
        /// <returns>The data received</returns>
        public string Read()
        {
            try
            {
                byte[] data = new byte[1024];
                int recv = server.Receive(data);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                return stringData;
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                return null;
            }
        }


        /// <summary>
        /// Starts the Thread that will be in charge of incomming Data</summary>
        public void ReceiveThread()
        {
            while (runThread)
            {
                    byte[] data = new byte[1024];
                    int recv = server.Receive(data);
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    playerMove = stringData;
                    Publish("Player_Moved");
            }
        }


        /// <summary>
        /// Disconnects the client from the Server</summary>
        public void Disconnect()
        {
            Connected = false;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}