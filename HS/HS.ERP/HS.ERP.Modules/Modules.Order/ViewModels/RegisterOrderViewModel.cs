using HS.ERP.Core;
using Prism.Mvvm;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Modules.Order.ViewModels
{
    public class RegisterOrderViewModel : BindableBase
    {
        private ObservableCollection<PersonTwo> _messages;
        
        private IDataManager<PersonTwo> DataManager { get; }

        public ObservableCollection<PersonTwo> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }

        public RegisterOrderViewModel(IDataManager<PersonTwo> dataManager)
        {
            this.DataManager = dataManager;

            Messages = new ObservableCollection<PersonTwo>()
            {
                 new PersonTwo{Name = "RegisterOrderViewModel"},
                new PersonTwo{Name = "RegisterOrderViewModel"},
                new PersonTwo{Name = "RegisterOrderViewModel"},
            };


            DataManager.AddRange(Messages);
        }
    }
}
