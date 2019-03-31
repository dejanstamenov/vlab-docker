using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace vlab_docker_dotnet_core_stream_app.ServerModes
{
    public class TCPServerAsync
    {
        private TcpListener serverListener;
        private int serverPortNumber;

        public TCPServerAsync() { }

        public TCPServerAsync(int serverPortNumber)
        {
            this.serverPortNumber = serverPortNumber;
            serverListener = new TcpListener(IPAddress.Any, this.serverPortNumber);
        }

        public async Task RunServerAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(serverListener.Stop);
            await Console.Out.WriteLineAsync($"Starting TCP server on port: {serverPortNumber}..");
            serverListener.Start();
            await Console.Out.WriteLineAsync($"TCP server started. Awaiting client connection.");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    TcpClient client = await serverListener.AcceptTcpClientAsync();
                    await HandleClientRequestAsync(client);
                }
                catch (ObjectDisposedException) when (cancellationToken.IsCancellationRequested)
                {
                    await Console.Out.WriteLineAsync("TcpListener stopped listening because cancellation was requested.");
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync($"Exception awaiting for new TCP connection to the server.\n{ex.Message}\n{ex.InnerException}\n{ex.StackTrace}");
                }
            }
        }

        private async Task HandleClientRequestAsync(TcpClient client)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(client.GetStream(), Encoding.UTF8))
                {
                    string clientMessage = await streamReader.ReadLineAsync();
                    await HandleRequest(client, clientMessage);
                }
            }
            catch (Exception ex)
            {
                if (!client.Connected)
                {
                    await Console.Out.WriteLineAsync($">> Client disconnected from server.");
                    return;
                }

                await Console.Out.WriteLineAsync($"Exception reading stream from client.\n{ex.Message}\n{ex.InnerException}\n{ex.StackTrace}");
            }
        }

        private async Task HandleRequest(TcpClient client, string clientMessage)
        {
            await Console.Out.WriteLineAsync($">> Client stream data from {client.Client.RemoteEndPoint as IPEndPoint}: {clientMessage}.");
            await SendMessageToClient(client);
            await DisconnectClient(client);
        }
        
        private async Task SendMessageToClient(TcpClient client)
        {
            await Console.Out.WriteLineAsync($">> Sending thank you note to client: {client.Client.RemoteEndPoint as IPEndPoint}..");

            if (client.Connected)
            {
                NetworkStream networkStream = client.GetStream();
                StreamWriter networkStreamWriter = new StreamWriter(networkStream);
                networkStreamWriter.WriteLine("Thank you!");
                networkStreamWriter.Flush();

                await Console.Out.WriteLineAsync(">> Message to the client sent!");
            }
            else
            {
                await Console.Out.WriteLineAsync(">> Cannot send message - client is disconnected.");
            }
        }

        private async Task DisconnectClient(TcpClient client)
        {
            if (client.Connected)
            {
                await Console.Out.WriteLineAsync($"Connection to client: {client.Client.RemoteEndPoint as IPEndPoint} is being closed..");
                client.Close();
            }
        }
    }
}