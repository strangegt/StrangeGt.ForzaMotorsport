using System;

using StrangeGt.ForzaMotorsport.Listener;

namespace StrangeGt.ForzaMotorsport.App.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private UDPData _Item = new UDPData();
        public ItemDetailViewModel(UDPData item )
        {
            Title = "UDPData";
            Item = item;
        }

        public UDPData Item { get => _Item; set  {
                SetProperty<UDPData>(ref _Item, value);
                
            }
        }
    }
}
