using System;
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
        public string generated { get; set; }


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
                Read();
                }
            catch (SocketException e) { Console.WriteLine("Unable to connect to server." + e.ToString()); }
        }



        /// <summary>
        /// This will send data to the Server </summary>
        /// <param name="command">The command to send to server</param>
        public void Send(string command)
        {
            server.Send(Encoding.ASCII.GetBytes(command));
        }


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
                Console.WriteLine("Connection with Server has been Terminated: Server Shut Down.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                return null;
            }
        }


        public void Disconnect()
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        
    }
}