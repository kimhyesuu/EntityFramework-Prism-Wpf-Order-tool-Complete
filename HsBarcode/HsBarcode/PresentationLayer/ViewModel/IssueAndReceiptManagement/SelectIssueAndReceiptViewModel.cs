using HsBarcode.Command;
using HsBarcode.PresentationLayer.Model.Enum;
using HsBarcode.PresentationLayer.View.BarcodeView;
using HsBarcode.PresentationLayer.View.Common;
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
   public class SelectIssueAndReceiptViewModel : BaseViewModel
   {
      private readonly GoodIssueManagementViewModel _goodIssueManagement;
      private readonly GoodReceiptManagementViewModel _goodReceiptManagement;
      private readonly IndexPagesModel _indexPagesModel;

      public ICommand SelectCurrentPagingCommand { set; private get; }

      public GoodIssueManagementViewModel GoodIssueManagement
      {
         get => _goodIssueManagement;
      }

      public GoodReceiptManagementViewModel GoodReceiptManagement
      {
         get => _goodReceiptManagement;
      }

      public IndexPages CurrentView
      {
         get => _indexPagesModel.CurrentView;
         set
         {
            _indexPagesModel.CurrentView = value;
            OnPropertyChanged(nameof(CurrentView));
         }
      }

      public SelectIssueAndReceiptViewModel()
      {               
         _indexPagesModel = new IndexPagesModel();        
         _goodIssueManagement = new GoodIssueManagementViewModel();
         _goodReceiptManagement = new GoodReceiptManagementViewModel();
         SelectCurrentPagingCommand = new RelayCommand(o => OnSwithPage(o));
      }

      public void OnSwithPage(object indexPages)=> CurrentView = (IndexPages)Enum.Parse(typeof(IndexPages), indexPages.ToString());
   }
}
