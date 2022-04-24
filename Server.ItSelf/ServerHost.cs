using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace Server.ItSelf
{
    public class ServerHost
    {
        private readonly IHandler _handler;
        public ServerHost(IHandler handler)
        {
            _handler = handler;
        }

        public void StartV1()
        {
            Console.WriteLine("Server started V1!");
            TcpListener listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();

            while (true)
            {
                using (var client = listener.AcceptTcpClient())
                using (var networkStream = client.GetStream())
                using (var reader = new StreamReader(networkStream))
                {
                    var firstLine = reader.ReadLine();
                    for (string line = null; line != string.Empty; line = reader.ReadLine())
                        ;

                    var request = RequestParser.Parse(firstLine);
                    _handler.Handle(networkStream, request);
                }
            }
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Server started Async!");
            TcpListener listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                await ProcessClientAsync(client);
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            using (client)
            using (var networkStream = client.GetStream())
            using (var reader = new StreamReader(networkStream))
            {
                var firstLine = await reader.ReadLineAsync();
                for (string line = null; line != string.Empty; line = await reader.ReadLineAsync())
                    ;

                var request = RequestParser.Parse(firstLine);
                await _handler.HandleAsync(networkStream, request);
            }
        }

        public void StartV2()
        {
            Console.WriteLine("Server started V2!");
            TcpListener listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();

            while (true)
            {
                var client = listener.AcceptTcpClient();
                ProcessClient(client);
            }
        }

        private void ProcessClient(TcpClient client)
        {
            ThreadPool.QueueUserWorkItem(obj =>
            {
                using (client)
                using (var networkStream = client.GetStream())
                using (var reader = new StreamReader(networkStream))
                {
                    var firstLine = reader.ReadLine();
                    for (string line = null; line != string.Empty; line = reader.ReadLine())
                        ;

                    var request = RequestParser.Parse(firstLine);
                    _handler.Handle(networkStream, request);
                }
            });
        }
    }
}
