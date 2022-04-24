using System;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ServerFromScrath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();

            var client = listener.AcceptTcpClient();
            using (var stream = client.GetStream())
            {
                using (var reader = new StreamReader(stream))
                using (var writer = new StreamWriter(stream))
                {
                    for (string line = null; line != string.Empty; line = reader.ReadLine())
                    {
                        Console.WriteLine(line);
                    }
                    writer.WriteLine("Hello from server!");
                }
            }
        }
    }
}
