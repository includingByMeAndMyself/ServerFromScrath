using Server.ItSelf;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServerFromScrath
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServerHost host = new ServerHost(new ControllersHandler(typeof(Program).Assembly));
            await host.StartAsync();
        }  
    }
}
