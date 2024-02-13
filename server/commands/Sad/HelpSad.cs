namespace OmniscientServer
{
    public class HelpSadCommand : Command
    {
        public HelpSadCommand(Server server) : base(server) { }

        protected override string ExecuteCommand(string[] parameters)
        {
             return "Available commands:\n" +
                    "---------------------\n" +
                    "help - Display a list of sad sub commands and their descriptions.\n" +
                    "program - Lists all programs.\n" +
                    "vulne - Lists vulnerable programs (need program command before).\n" +
                    "system - Get system infos.";
        }
    }
}