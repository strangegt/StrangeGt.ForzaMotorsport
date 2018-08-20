using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrangeGt.ForzaMotorsport.Listener
{
    public class UDPListener
    {
        private const int listenPort = 11000;
        public async Task StartListenerAsync(Action<UDPDataEventArgs> action, CancellationToken cancellationToken)
        {
            bool done = false;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
                while (!done)
                {
                    Task<UdpReceiveResult> task = listener.ReceiveAsync().WithCancellation<UdpReceiveResult>(cancellationToken);
                    UdpReceiveResult resul = await task;
                    action.Invoke(new UDPDataEventArgs(resul));
                    if (cancellationToken.IsCancellationRequested)
                    {
                        done = true;
                    }

                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }

    }
}
