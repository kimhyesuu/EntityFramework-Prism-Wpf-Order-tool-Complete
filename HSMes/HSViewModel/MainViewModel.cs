using HSFramework.Base;
using HSUiModel.Enum.PageControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HSViewModel
{
   public class MainViewModel : BaseViewModel
   {

      private readonly ChangePage _indexPages;

      public ICommand PageControlCommand { set; private get; }

      public IndexPages CurrentView
      {
         get => _indexPages.CurrentView;
         set
         {
            _indexPages.CurrentView = value;
            OnPropertyChanged(nameof(CurrentView));
         }
      }

      public MainViewModel()
      {
         PageControlCommand = new RelayCommand(o => OnSwithPage(o));
      }

      public void OnSwithPage(object indexPages) => CurrentView = (IndexPages)Enum.Parse(typeof(IndexPages), indexPages.ToString());

   }
}
