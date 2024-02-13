namespace OmniscientServer
{
    public class HelpCommand : Command
    {
        public HelpCommand(Server server) : base(server) { }

        protected override string ExecuteCommand(string[] parameters)
        {
            return "Available commands:\n" +
                    "help - Display a list of commands and their descriptions.\n" +
                    "list - List available clients.\n" +
                    "use <client_id> - Connect to a client.\n" +
                    "exec <command> - Execute a command on the client.\n" +
                    "close - Close the connection to the client.\n" +
                    "exit - Close the server.\n" +
                    "get-log - Get the log file from the client.\n" +
                    "sad - sub command (sad help for more infos).";
        }
    }
}