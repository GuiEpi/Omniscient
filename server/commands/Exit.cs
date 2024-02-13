namespace OmniscientServer
{
    public class ExitCommand : Command
    {
        public ExitCommand(Server server) : base(server) { }

        protected override string ExecuteCommand(string[] parameters)
        {
            System.Environment.Exit(0);
            return "Exiting...";
        }
    }
}