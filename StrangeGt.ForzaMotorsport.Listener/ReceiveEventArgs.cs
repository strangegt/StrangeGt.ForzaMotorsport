using System;
using System.Net.Sockets;

namespace StrangeGt.ForzaMotorsport.Listener
{
    public class ReceiveEventArgs : EventArgs
    {
      
        public ReceiveEventArgs(UdpReceiveResult resul):base()
        {
            this.UdpReceiveResult = resul;
        }

        public UdpReceiveResult UdpReceiveResult { get; private set; }
    }
}