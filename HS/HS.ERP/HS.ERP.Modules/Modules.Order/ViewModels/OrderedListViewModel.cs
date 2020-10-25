using Prism.Mvvm;

namespace Modules.Order.ViewModels
{
    public class OrderedListViewModel : BindableBase
    {
        private string _messageReceived = "Hello";
        public string MessageReceived
        {
            get { return _messageReceived; }
            set { SetProperty(ref _messageReceived, value); }
        }
    }
}
