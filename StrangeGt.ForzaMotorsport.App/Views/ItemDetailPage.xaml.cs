using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StrangeGt.ForzaMotorsport.App.ViewModels;
using System.Threading;
using StrangeGt.ForzaMotorsport.Listener;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace StrangeGt.ForzaMotorsport.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
    
        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();
            UDPData item = new UDPData();
         //   viewModel = new ItemDetailViewModel(item);
         //   BindingContext = viewModel;
        }

      

       

      
    }
}