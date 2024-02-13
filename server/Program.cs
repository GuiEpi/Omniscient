using System;

namespace OmniscientServer
{
    class Program
    {
        const string dataPath = @".\data";
        const string version = "1.0.0";
        const string author = "Guillaume/John";
        const string asciiArtPath = "utils/ascii_art.txt";
        const string welcomeMessage = "Welcome to Omniscient C2 CLI Server!";
        static void Main(string[] args)
        {
            Console.WriteLine(File.ReadAllText(asciiArtPath));
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(version);
            Console.WriteLine(author);
            Console.WriteLine("\n\n");
            Server server = new Server("192.168.1.39", 13000, dataPath);
            server.Start();
        }
    }
}
