using Server.ItSelf;
using System;
using System.IO;

namespace ServerFromScrath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerHost host = new ServerHost(new ControllersHandler(typeof(Program).Assembly));
            host.StartV2();
        }  
    }
}
