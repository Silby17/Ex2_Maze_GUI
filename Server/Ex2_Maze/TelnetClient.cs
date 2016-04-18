using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex2_Maze
{
    public class TelnetClient : ITelnetClient
    {
        private int PORT;
        private IPAddress IP;
        private Socket server;
        private IPEndPoint ipep;
        private Thread receiverThread;

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
            }
            catch (SocketException e) { Console.WriteLine("Unable to connect to server." + e.ToString()); }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public string Read()
        {
            throw new NotImplementedException();
        }

        public void Write(string commnad)
        {
            throw new NotImplementedException();
        }
    }
}
