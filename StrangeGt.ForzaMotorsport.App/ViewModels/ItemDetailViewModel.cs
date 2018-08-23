using StrangeGt.ForzaMotorsport.Listener;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace StrangeGt.ForzaMotorsport.App.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private UDPData _Item = new UDPData();
        private UDPListener listener;
        public ICommand StartListener { get; set; }
        public ICommand StopListener { get; set; }

        public ItemDetailViewModel()
        {
        Title = "UDPData";
            StartListener = new Command((object arg) =>
            {
                if (listener == null)
                {
                    listener = new UDPListener();
                    listener.OnReceive += Listener_OnReceive;
                    listener.OnStart += Listener_OnStart;
                    listener.OnStop += Listener_OnStop;
                }
                listener.StartListener();
            },CanExecuteStartListener);
            StopListener = new Command((object arg) =>
            {
                if (listener != null)
                {
                    listener.StopListener();
                }
            },CanExecuteStopListener);
            
        }

        private void Listener_OnStop(object sender, EventArgs e)
        {
            ChangeCanExecute();
        }

        private void Listener_OnStart(object sender, StartEventArgs e)
        {
            ChangeCanExecute();
        }

        private void ChangeCanExecute()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                (StartListener as Command).ChangeCanExecute();
                (StopListener as Command).ChangeCanExecute();
            });
            //OnPropertyChanged("StartListener");
            //OnPropertyChanged("StopListener");
            //StartListener.
        }

        public ItemDetailViewModel(UDPData item):this()
        {
            Item = item;
        }

        private bool CanExecuteStartListener(object arg)
        {
            return (!listener?.IsListening)??true;
        }

        private bool CanExecuteStopListener(object arg)
        {
            return (listener?.IsListening) ?? false;
            
        }

        public UDPData Item
        {
            get => _Item; set
            {
                SetProperty<UDPData>(ref _Item, value);

            }
        }
        private void Listener_OnReceive(object sender, ReceiveEventArgs e)
        {
            UDPData data = e.UdpReceiveResult.Buffer.Deserialize<UDPData>();
            WriteData(data);
        }
        private void WriteData(UDPData data)
        {
            Item = data;
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
        }
     }
}
