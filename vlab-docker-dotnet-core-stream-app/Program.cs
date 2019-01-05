using System;

namespace vlab_docker_dotnet_core_stream_app
{
    class Program
    {
        static void Main(string[] args)
        { 
#if DEBUG
            args = new[] { "80" };
#endif

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet run -p .\vlab-docker-dotnet-core-stream-app.csproj <port-number>");
                return;
            }

            TCPServerSync tcpServerSync = new TCPServerSync(Convert.ToInt32(args[0]));
        }
    }
}