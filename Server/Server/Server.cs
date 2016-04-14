using System.Threading.Tasks;
using System.Net.Sockets;
using System;
using System.Net;

namespace Server
{
    public class Server
    {
        private int PORT;
        //private IView view;
        //private IPresenter presenter;
        private IModel model;

        /// <summary>
        /// The Server constructor</summary>
        public Server()
        {
            //Reads the port from the ConfigFile
            this.PORT = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Port"]);
            
            //Creates new Model
            this.model = new Model();

            //Creates new Presenter and passes it the Model
            //this.presenter = new ServerPresenter(model);
        }


        /// <summary>
        /// This method starts the running of the Server</summary>
        public void StartServer()
        {
            Console.WriteLine("Server Started");
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, PORT);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            int clientNum = 0;

            //Keeps listening for more Client connection requests
            while (true)
            {
                Socket client = newsock.Accept();
                Console.WriteLine("Client# {0} accepted!", ++clientNum);

                //Create new view which will handle to Client
                View clientHandler = new View(client, model);
                Task.Factory.StartNew(clientHandler.handle);
            }
        }
    }
}