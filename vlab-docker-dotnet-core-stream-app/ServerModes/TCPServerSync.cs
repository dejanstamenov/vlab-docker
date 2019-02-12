using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace vlab_docker_dotnet_core_stream_app.ServerModes
{
    public class TCPServerSync
    {
        private TcpListener serverListener;
        private bool acceptUserRequests;
        private int serverPortNumber;

        public TCPServerSync() { }

        public TCPServerSync(int serverPortNumber)
        {
            this.serverPortNumber = serverPortNumber;
            acceptUserRequests = true;
            serverListener = new TcpListener(IPAddress.Any, this.serverPortNumber);
        }
        
        public void RunServerSync()
        {
            try
            {
                Console.WriteLine($"Starting TCP server on port: {serverPortNumber}..");
                serverListener.Start();
                Console.WriteLine($"TCP server started. Awaiting client connection.");

                while (acceptUserRequests)
                {
                    try
                    {
                        var client = serverListener.AcceptTcpClient();
                        Console.WriteLine($"New TCP connection established from {client.Client.RemoteEndPoint as IPEndPoint}.");
                        HandleClientRequestSync(client);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Exception awaiting for new TCP connection to the server.\n{ex.Message}\n{ex.InnerException}\n{ex.StackTrace}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Exception starting the TCP server.\n{ex.Message}\n{ex.InnerException}\n{ex.StackTrace}");
            }
        }

        private void HandleClientRequestSync(TcpClient client)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(client.GetStream(), Encoding.UTF8))
                {
                    string clientMessage = string.Empty;

                    while ((clientMessage = streamReader.ReadLine()) != null)
                        HandleRequest(client, clientMessage);
                }
            }
            catch (Exception ex)
            {
                if (!client.Connected)
                {
                    Console.WriteLine($">> Client disconnected from server.");
                    return;
                }

                Console.Error.WriteLine($"Exception reading stream from client.\n{ex.Message}\n{ex.InnerException}\n{ex.StackTrace}");
            }
        }

        private void HandleRequest(TcpClient client, string clientMessage)
        {
            Console.WriteLine($">> Client stream data from {client.Client.RemoteEndPoint as IPEndPoint}: {clientMessage}.");
            SendMessageToClient(client);
            DisconnectClient(client);
        }
        
        private void SendMessageToClient(TcpClient client)
        {
            Console.WriteLine($">> Sending thank you note to client: {client.Client.RemoteEndPoint as IPEndPoint}..");

            if (client.Connected)
            {
                NetworkStream networkStream = client.GetStream();
                StreamWriter networkStreamWriter = new StreamWriter(networkStream);
                networkStreamWriter.WriteLine("Thank you!");
                networkStreamWriter.Flush();

                Console.WriteLine(">> Message to the client sent!");
            }
            else
            {
                Console.WriteLine(">> Cannot send message - client is disconnected.");
            }
        }

        private void DisconnectClient(TcpClient client)
        {
            if (client.Connected)
            {
                client.Close();
                Console.WriteLine($"Connection to client: {client.Client.RemoteEndPoint as IPEndPoint} is closed.");
            }
        }
    }
}
