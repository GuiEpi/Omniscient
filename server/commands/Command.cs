namespace OmniscientServer
{
    public interface ICommand
    {
        string Execute(string[] parameters);
    }

    public abstract class Command : ICommand
    {
        protected Server server;
        public Command(Server server)
        {
            this.server = server;
        }

        public string Execute(string[] parameters)
        {
            if (this.RequiresClientConnection() && !this.server.isConnected)
            {
                return "Omniscient$ No client is currently connected.";
            }

            return this.ExecuteCommand(parameters);
        }

        protected abstract string ExecuteCommand(string[] parameters);

        protected virtual bool RequiresClientConnection()
        {
            return false;
        }
    }
}
