using System.Text.RegularExpressions;

namespace OmniscientServer
{
    public class ScanProgramsCommand : ExecuteOnClientCommand
    {
        public ScanProgramsCommand(Server server) : base(server) { }

        protected override string ExecuteCommand(string[] parameters)
        {
            string[] queryCmd = { 
                "exec", 
                "reg",
                "query",
                @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", 
                "/s", 
                "|",
                "findstr",
                "/i",
                "/c:\"DisplayName\"",
                "/c:\"DisplayVersion\"",
                "/c:\"Publisher\"",
                "/c:\"InstallDate\"",
                "/c:\"security update\"",
                "/c:\"hotfix\"",
                "/c:\"update\"",

            };

            string output = base.ExecuteCommand(queryCmd);
            string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();
            int flag = 0;

            foreach (string line in lines)
            {
                if (line.Contains("DisplayName") && flag == 0)
                {
                    result.Add(line);
                    flag = 1;
                }

                if (line.Contains("DisplayVersion") && flag == 1)
                {
                    result.Add(line);
                    flag = 0;
                }
            }

            string pattern = @"REG_\S*";
            for (int i = 0; i < result.Count; i++)
            {
                string splitter = Regex.Match(result[i], pattern).ToString();
                if (i % 2 == 0)
                {   
                    Writer(result[i].Split(splitter)[1].Trim() + " ---- ", "scan_programs.txt");
                }
                else
                {   
                    Writer(result[i].Split(splitter)[1].Trim(), "scan_programs.txt", true);
                }
            }

            return $"Omniscient$ Scan programs have been imported successfully.";
        }
    }
}