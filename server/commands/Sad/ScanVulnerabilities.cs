using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OmniscientServer
{
    public class ScanVulnerabilitiesCommand : ExecuteOnClientCommand 
    {
        private List<string> scannedPrograms = new List<string>();
        public ScanVulnerabilitiesCommand(Server server) : base(server) { }

        protected override string ExecuteCommand(string[] parameters)
        {
            if (!File.Exists($@"{server.clientDataPath}\scan_programs.txt"))
            {
                return "Omniscient$ No programs have been scanned yet. Please scan programs first.";
            }
            string clientProgramsPath = $@"{server.clientDataPath}\scan_programs.txt";
            string[] lines = File.ReadAllLines(clientProgramsPath);

            string outputFileName = $@"{server.clientDataPath}\scan_vulnerabilities.txt";
            using (StreamWriter outputFile = new StreamWriter(outputFileName))
            {
                try
                {
                    Console.Write("Scanning programs... ");
		            using (var progress = new ProgressBar()) {
                        int count = 0;
                        foreach (string line in lines)
                        {
                            count++;
                            progress.Report((double) count / lines.Length);
                            string programName = line.Split("----")[0].Trim().Split(" ")[0];
                            string programVersion = line.Split("----")[1].Trim();

                            if (scannedPrograms.Contains(programName.ToLower()))
                            {
                                continue;
                            }

                            if (programName == "Package")
                            {
                                continue;
                            }

                            scannedPrograms.Add(programName.ToLower());

                            var client = new RestClient($"https://www.opencve.io/api/cve?search={programName}&cvss=critical");
                            var request = new RestRequest();
                            request.AddHeader("Authorization", "Basic cHVzdHVmbGU6eWFoUzlGclJnWjU3Slgh");
                            request.AddHeader("Cookie", "SERVERID170368=55a1b3c3|ZEJJH|ZEJJH; session=eyJfcGVybWFuZW50Ijp0cnVlfQ.ZEJJHA.l3gdpMgJkg2uUst7iu6A15ZZU-c");

                            var response = client.Execute(request);

                            try
                            {
                                var json = JsonConvert.DeserializeObject(response.Content);
                                var jsonArray = JArray.Parse(json.ToString());

                                foreach (var item in jsonArray)
                                {
                                    var id = item["id"];
                                    var summary = item["summary"];

                                    if (jsonArray.IndexOf(item) == 0)
                                    {
                                        outputFile.WriteLine("#############################################");
                                        outputFile.WriteLine($"{programName}\r");
                                    }

                                    outputFile.WriteLine(id);
                                    outputFile.WriteLine($"{summary}\r");

                                    if (jsonArray.IndexOf(item) == jsonArray.Count - 1)
                                    {
                                        outputFile.WriteLine("#############################################\r");
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error has occurred: {ex.Message}");
                    Console.WriteLine("Problem while retrieving information");
                }
            }

            return $"Omniscient$ Vulnerabilities have been scanned successfully.";
        }
    }
}
