using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StrangeGt.ForzaMotorsport.App.ViewModels;
using System.Threading;
using StrangeGt.ForzaMotorsport.Listener;
using System.Threading.Tasks;

namespace StrangeGt.ForzaMotorsport.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        private bool listening;
        private CancellationTokenSource tokenSource;
        private Task listeningTask;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            UDPData item=new UDPData();

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            StartListener();
        }
        private void StartListener()
        {
        
            listening = true;
            tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;
            cancellationToken.Register(() =>
            {

            });
            listeningTask = Task.Run(() =>
            {
                // CreateDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StrangeGt.ForzaMotorsport", "databases", string.Format("Forzamotorsport_{0:yyyyMMddHHmmss}.sqlite", DateTime.Now.ToUniversalTime())));

                UDPListener listener = new UDPListener();
                listener.StartListenerAsync(new Action<UDPDataEventArgs>(UDPDataHandler), cancellationToken).GetAwaiter().GetResult(); ;
                listening = false;
                // CloseDB();

            }, cancellationToken);
        }
        private void UDPDataHandler(UDPDataEventArgs args)
        {
            // Console.Clear();
            UDPData data = args.UdpReceiveResult.Buffer.Deserialize<UDPData>();
            WriteData(data);
        }
        private void WriteData(UDPData data)
        {
            //  UDPDataView view = this.FindByName<UDPDataView>("UDPDataView");
            viewModel.Item = data;
            
           
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            tokenSource.Cancel(true);
        }
    }
}