using HsBarcode.Command;
using HsBarcode.PresentationLayer.Model.Enum;
using HsBarcode.PresentationLayer.ViewModel.Base;
using HsBarcode.PresentationLayer.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HsBarcode.PresentationLayer.ViewModel.IssueAndReceiptManagement
{
   public class GoodIssueManagementViewModel : BaseViewModel
   {     
      public ICommand PageBackCommand { set; private get; }

      public GoodIssueManagementViewModel()
      {       
         PageBackCommand = new RelayCommand(o => OnSwithPage(o));
      }

      private void OnSwithPage(object o)
      {
         View.BarcodeView.CheckingBarcodeTool d = new View.BarcodeView.CheckingBarcodeTool();
         d.DataContext = new SelectIssueAndReceiptViewModel();      
      }
   }
}
