using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrangeGt.ForzaMotorsport.Listener
{
    public class UDPListener
    {
        private CancellationTokenSource tokenSource;
        private Task listeningTask;

        public bool IsListening { get; private set; }
        public int Port { get; set; } = 11000;
        public event EventHandler<ReceiveEventArgs> OnReceive;
        public event EventHandler<StartEventArgs> OnStart;
        public event EventHandler OnStop;
        public event EventHandler<ExceptionEventArgs> OnException;
        public void StartListener()
        {
            if (IsListening)
            {
                return;
            }
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                tokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = tokenSource.Token;
                listeningTask = Task.Run(() =>
                {
                    IsListening = true;
                    try
                    {
                        StartListenerAsync(cancellationToken).GetAwaiter().GetResult(); ;
                    }
                    finally
                    {
                        IsListening = false;
                        OnStop?.Invoke(this, EventArgs.Empty);
                    }

                }, cancellationToken);
            }
        }
        private async Task StartListenerAsync(CancellationToken cancellationToken)
        {
            bool done = false;
            UdpClient client = new UdpClient(Port);
            try
            {
                OnStart?.Invoke(this, new StartEventArgs(client));
                while (!done)
                {
                    // Task<UdpReceiveResult> task =
                    UdpReceiveResult resul = await client.ReceiveAsync().WithCancellation<UdpReceiveResult>(cancellationToken);

                    //   await task;
                    OnReceive?.Invoke(this, new ReceiveEventArgs(resul));
                    if (cancellationToken.IsCancellationRequested)
                    {
                        done = true;
                    }
                }
            }
            catch (Exception e)
            {
                OnException?.Invoke(this, new ExceptionEventArgs(e));
            }
            finally
            {
                client.Close();
             }
        }

        public void StopListener()
        {
            if (!tokenSource.IsCancellationRequested)
            {
                tokenSource.Cancel(true);
            }
        }
    }
}
