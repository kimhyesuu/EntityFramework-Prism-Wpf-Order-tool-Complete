using System;
using System.Collections.ObjectModel;
using System.Windows;
using HS.ERP.Business.Models;
using HS.ERP.Business.Models.Enums;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace HS.ERP.Outlook.Core.Dialogs.ViewModels
{
   public class ProductInfoViewModel : BindableBase, IDialogAware
   {
      private Product _productInfo;
      private ObservableCollection<Product> _productList;

      public ObservableCollection<Product> Products
      {
         get { return _productList; }
         set { SetProperty(ref _productList, value); }
      }

      public Product ProductInfo
      {
         get { return _productInfo; }
         set { SetProperty(ref _productInfo, value); }
      }

      public bool CanSaveExcute
      {
         get => true;
         set => SaveProductInfoCommand.RaiseCanExecuteChanged();
      }

      private void ProductInfoInit()
      {
         ProductInfo = null;
         ProductInfo = new Product(Newid());
         ProductInfo.EntityState = EntityStateOption.None;
      }

      public ProductInfoViewModel()
      {
         DataInitialize();
         CommandInitialize();  
      }
      
      private void CommandInitialize()
      {
         CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
         SaveProductInfoCommand = new DelegateCommand<string>(AddOrUpdate).ObservesCanExecute(() => CanSaveExcute);
         DeleteProductInfoCommand = new DelegateCommand<object>(DeleteProduct);
      }

      private void DataInitialize()
      {
         Products = new ObservableCollection<Product>();
         ProductInfoInit();
      }

      public DelegateCommand<string> SaveProductInfoCommand { get; private set; }
      public DelegateCommand<object> DeleteProductInfoCommand { get; private set; }

      private void DeleteProduct(object selectedList)
      {
         if (selectedList is null) return;

         var selectedProduct = selectedList as Product;

         if (MessageBox.Show($"{selectedProduct.ProductName}을 삭제하시겠습니까?"
            , "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            Products.Remove(selectedProduct);
         }
      }

      private void AddOrUpdate(string product)
      {
         string[] productInfo = product.Split(':');

         var receivedInfo = new Product
         {
            ProductId = ProductInfo.ProductId,
            ProductName = productInfo[0],
            ProductPrice = productInfo[1],
            Series = productInfo[2],
            CoreProcessor = productInfo[3],
            CoreSize = productInfo[4],
            Connectivity = productInfo[5],
            Speed = productInfo[6],
            NumberOfIO = productInfo[7],
            Peripherals = productInfo[8],
            ProgramMemoryType = productInfo[9],
            ProgramMemorySize = productInfo[10],
            RamSize = productInfo[11],
            EEPROMSize = productInfo[12],
            DataConverter = productInfo[13],
            VoltageSupply = productInfo[14],
            OperatingTemperature = productInfo[15],
            OscillatorType = productInfo[16],
            PakageCase = productInfo[17],
            EntityState = ProductInfo.EntityState
         };

         if (IsAdd(receivedInfo))
         {
            AddProductInfo(receivedInfo);
         }
         else
         {
            UpdateProductInfo(receivedInfo);
         }
      }

      private void UpdateProductInfo(Product receivedInfo)
      {
         receivedInfo.EntityState = EntityStateOption.Updated;
         Products.Insert(Products.IndexOf(ProductInfo), receivedInfo);
         Products.Remove(ProductInfo);

         receivedInfo = null;
         ProductInfoInit();
      }

      private void AddProductInfo(Product receivedInfo)
      {
         Products.Add(new Product
         {
            ProductName = receivedInfo.ProductName,
            ProductPrice = receivedInfo.ProductPrice,
            Series = receivedInfo.Series,
            CoreProcessor = receivedInfo.CoreProcessor,
            CoreSize = receivedInfo.CoreSize,
            Connectivity = receivedInfo.Connectivity,
            Speed = receivedInfo.Speed,
            NumberOfIO = receivedInfo.NumberOfIO,
            Peripherals = receivedInfo.Peripherals,
            ProgramMemoryType = receivedInfo.ProgramMemoryType,
            ProgramMemorySize = receivedInfo.ProgramMemorySize,
            RamSize = receivedInfo.RamSize,
            EEPROMSize = receivedInfo.EEPROMSize,
            DataConverter = receivedInfo.DataConverter,
            VoltageSupply = receivedInfo.VoltageSupply,
            OperatingTemperature = receivedInfo.OperatingTemperature,
            OscillatorType = receivedInfo.OscillatorType,
            PakageCase = receivedInfo.PakageCase,
            EntityState = EntityStateOption.Inserted,
            CreatedDate = DateTime.Now
         });

         receivedInfo = null;
         ProductInfoInit();
      }

      private bool IsAdd(Product receivedInfo)
      => receivedInfo.EntityState == EntityStateOption.None;

      private long? Newid()
      {
         var rd = new Random();
         return long.Parse(DateTime.Now.ToString("yyyyMMdd") + rd.Next(1, 1000));
      }

      #region 다이얼로그 값 전달하는 방식 여기 완성 되면 다음 단계로 설정
      public DelegateCommand<string> CloseDialogCommand { get; private set; }

      public event Action<IDialogResult> RequestClose;

      private ObservableCollection<object> _messagesManage;


      public ObservableCollection<object> MessagesManage
      {
         get { return _messagesManage; }
         set { SetProperty(ref _messagesManage, value); }
      }

    

      private void CloseDialog(string parameter)
      {
         ButtonResult result = ButtonResult.None;
         var transportParameter = new DialogParameters();
         string parameterValue = string.Empty;

         if (parameter?.ToLower() == "true")
         {
            result = ButtonResult.OK;
            parameterValue = "ButtonResult.OK";
         }

         transportParameter.Add("submessage", parameterValue);
         RaiseRequestClose(result, transportParameter);
      }

      private void RaiseRequestClose(ButtonResult dialogResult, IDialogParameters dialogParameters)
         => RequestClose?.Invoke(new DialogResult(dialogResult, dialogParameters));

      public bool CanCloseDialog()
      {
         return true;
      }

      public void OnDialogClosed()
      {

      }

      public void OnDialogOpened(IDialogParameters parameters)
      {
         //Message = parameters.GetValue<string>("message");
      }
      #endregion

      #region 리소스
      private string _message;
      public string Message
      {
         get => _message;
         set { SetProperty(ref _message, value); }
      }
      public string ButtonOKTitle { get => "OK"; }
      public string ButtonCancelTitle { get => "Cancel"; }

      public string Title => "제품 정보";
      public int DialogWindowWith => 1200;

      #endregion
   }
}
