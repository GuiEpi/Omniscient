namespace OmniscientServer
{
    public class CloseCommand : ExecuteOnClientCommand
    {
        public CloseCommand(Server server) : base(server) { }

        protected override string ExecuteCommand(string[] parameters)
        {
            int clientIdToDisconnect;
            if (parameters.Length == 1)
            {
                if (server.isConnected)
                {
                    clientIdToDisconnect = server.clientId;
                    server.isConnected = false;
                    string[] command = { "exec", "exit" };
                    base.ExecuteCommand(command);
                    return $"Omniscient$ Closing connection to client {clientIdToDisconnect}...";
                }
                else
                {
                    return "Omniscient$ No client is currently connected.";
                }
            }
            else if (!int.TryParse(parameters[1], out clientIdToDisconnect))
            {
                return "Omniscient$ Invalid client ID.";
            }

            if (!server.clients.ContainsKey(clientIdToDisconnect))
            {
                return $"Omniscient$ No client with ID {clientIdToDisconnect} is connected.";
            }

            server.clients[clientIdToDisconnect].Close();

            return $"Omniscient$ Closed connection to client {clientIdToDisconnect}...";
        }

        protected override bool RequiresClientConnection()
        {
            return false;
        }
    }
}