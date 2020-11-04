using HsBarcode.PresentationLayer.View.Common;
using HsBarcode.PresentationLayer.ViewModel.Base;
using HsBarcode.PresentationLayer.ViewModel.Common;
using HsBarcode.PresentationLayer.ViewModel.IssueAndReceiptManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HsBarcode.PresentationLayer.ViewModel.Main
{
   public class MainViewModel : BaseViewModel
   {
      SelectIssueAndReceiptViewModel _selectIssueAndReceipt;
      MainPageClickContentVeiwModel _mainPageClickContent;

      public MainPageClickContentVeiwModel MainPageClickContent
      {
         get => _mainPageClickContent;
      }

      public SelectIssueAndReceiptViewModel SelectIssueAndReceipt
      {
         get => _selectIssueAndReceipt;
      }

      public MainViewModel()
      {
         _selectIssueAndReceipt = new SelectIssueAndReceiptViewModel();
         _mainPageClickContent = new MainPageClickContentVeiwModel();
      }
   }
}
