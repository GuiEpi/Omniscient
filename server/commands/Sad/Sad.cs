namespace OmniscientServer
{
    public class SadCommand : Command
    {
        private Dictionary<string, ICommand> subCommands;

        public SadCommand(Server server) : base(server)
        {
            subCommands = new Dictionary<string, ICommand>
            {
                { "system", new SystemInfosCommand(server) },
                { "program", new ScanProgramsCommand(server) },
                { "vulne", new ScanVulnerabilitiesCommand(server) },
                { "help", new HelpSadCommand(server) },
            };
        }

        protected override string ExecuteCommand(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                return "Please specify a subcommand.";
            }

            string subCommandName = parameters[1].ToLower();

            if (subCommands.ContainsKey(subCommandName))
            {
                return subCommands[subCommandName].Execute(parameters.Skip(2).ToArray());
            }
            else
            {
                return "Invalid subcommand.";
            }
        }

        protected override bool RequiresClientConnection()
        {
            return true; // Assuming that the "sad" command requires a client connection
        }
    }
}
