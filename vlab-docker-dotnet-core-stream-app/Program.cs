using System;
using System.Threading;
using System.Threading.Tasks;
using vlab_docker_dotnet_core_stream_app.ServerModes;

namespace vlab_docker_dotnet_core_stream_app
{
    class Program
    {
        static async Task Main(string[] args)
        { 
#if DEBUG
            args = new[] { "80" };
#endif

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet run -p .\vlab-docker-dotnet-core-stream-app.csproj <port-number>");
                args = new[] { "80" };
            }

            // Uncomment the lines below to use the TCP sync server. Then change the Main method into void Main declaration.
            // TCPServerSync tcpServerSync = new TCPServerSync(Convert.ToInt32(args[0]));
            // tcpServerSync.RunServerSync();

            // Uncomment the lines below to use the TCP async server. Then change the Main method into async Task Main declaration.
            TCPServerAsync tcpServerSync = new TCPServerAsync(Convert.ToInt32(args[0]));
            await tcpServerSync.RunServerAsync(CancellationToken.None);
        }
    }
}