using System.Net.Sockets;
using System.Text;
using System.Collections.Concurrent;

public class HandleDisconnect
{
    private TcpClient client;
    private int clientId;
    private ConcurrentDictionary<int, TcpClient> clients;

    public HandleDisconnect(TcpClient client, int clientId, ConcurrentDictionary<int, TcpClient> clients)
    {
        this.client = client;
        this.clientId = clientId;
        this.clients = clients;
    }

    public void Start()
    {
        new Thread(() =>
        {
            byte[] buffer = new byte[1024];
            var stream = client.GetStream();

            while (true)
            {
                if (!IsClientConnected(client))
                {
                    Console.WriteLine($"Omniscient$ Client with ID {clientId} unexpectedly disconnected.");
                    clients.TryRemove(clientId, out TcpClient removedClient);
                    client.Close();
                    break;
                }
                if (stream.DataAvailable)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    if (message.Trim() == "exit")
                    {
                        Console.WriteLine($"Omniscient$ Client with ID {clientId} disconnected.");
                        clients.TryRemove(clientId, out TcpClient removedClient);
                        client.Close();
                        break;
                    }
                }
            }
        }).Start();
    }

    private bool IsClientConnected(TcpClient client)
    {
        try
        {
            if (client != null && client.Client != null && client.Client.Connected)
            {
                /* part 1: check if socket is readable */
                if (client.Client.Poll(0, SelectMode.SelectRead))
                {
                    byte[] buff = new byte[1];
                    if (client.Client.Receive(buff, SocketFlags.Peek) == 0)
                    {
                        // client disconnected
                        return false;
                    }
                }

                return true;
            }
            else
            {
                // client is null or not connected
                return false;
            }
        }
        catch
        {
            // error occurred, client likely not connected
            return false;
        }
    }
}
