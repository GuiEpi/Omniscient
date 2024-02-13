namespace OmniscientServer
{
    public class ListClientCommand : Command
    {
        public ListClientCommand(Server server) : base(server) { }

        protected override string ExecuteCommand(string[] parameters)
        {
            if (server.clients.Count == 0)
            {
                return "No clients connected.";
            }
            
            string availableClients = "\tAvailable clients:\n" + 
                                        "\tId \t IP \t\t Port\n" + 
                                        "\t----- \t ---------- \t ----\n";
            foreach (var client in server.clients)
            {   
                string ip = client.Value.Client.RemoteEndPoint.ToString().Split(':')[0];
                string port = client.Value.Client.RemoteEndPoint.ToString().Split(':')[1];
                availableClients += $"\t{client.Key} \t {ip} \t {port}\n";
            }
            return availableClients;
        }
    }
}