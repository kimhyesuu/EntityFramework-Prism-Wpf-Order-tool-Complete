using HS.ERP.Core;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Modules.Order.ViewModels
{
    public class OrderedListViewModel : BindableBase
    {
        private ObservableCollection<Person> _messages;

        private IDataManager<Person> DataManager { get; }

        public ObservableCollection<Person> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }

        public OrderedListViewModel(IDataManager<Person> dataManager)
        {
            this.DataManager = dataManager;

            Messages = new ObservableCollection<Person>()
            {
                new Person{Name = "OrderedListViewModel"},
                new Person{Name = "OrderedListViewModel"},
                new Person{Name = "굿"},
            };

            DataManager.AddRange(Messages);
        }
    }
}
