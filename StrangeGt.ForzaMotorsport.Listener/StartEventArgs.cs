using System;
using System.Net.Sockets;

namespace StrangeGt.ForzaMotorsport.Listener
{
    public class StartEventArgs:EventArgs
    {
        public StartEventArgs(UdpClient cliemt)
        {
            Client = cliemt;
        }
        public UdpClient Client { get; private set; }
    }
}