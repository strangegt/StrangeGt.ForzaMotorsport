using System;
using System.Net.Sockets;

namespace StrangeGt.ForzaMotorsport.Listener
{
    public class UDPDataEventArgs : EventArgs
    {
      
        public UDPDataEventArgs(UdpReceiveResult resul)
        {
            this.UdpReceiveResult = resul;
        }

        public UdpReceiveResult UdpReceiveResult { get; private set; }
    }
}