using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
      #region 필드
      private Product _productInfo;
      private ObservableCollection<Product> _productList;
      #endregion

      #region 프로퍼티 
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

      public List<Product> DeletedProducts { get; set; }

      public bool CanSaveExcute
      {
         get => true;
         set => MoveProductInfoToListCommand.RaiseCanExecuteChanged();
      }
      #endregion

      #region Constructor
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
         SaveProductListCommand = new DelegateCommand<string>(SaveProductListDialog);
         MoveProductInfoToListCommand = new DelegateCommand<string>(AddOrUpdate).ObservesCanExecute(() => CanSaveExcute);
         DeleteProductInfoCommand = new DelegateCommand<object>(DeleteProduct);
      }

      private void DataInitialize()
      {
         DeletedProducts = new List<Product>();
         Products = new ObservableCollection<Product>();
         ProductInfoInit();
      }
      #endregion

      public DelegateCommand<string> MoveProductInfoToListCommand { get; private set; }
      public DelegateCommand<object> DeleteProductInfoCommand { get; private set; }
      public DelegateCommand<string> SaveProductListCommand { get; private set; }

      #region 제품 CRUD
      private void DeleteProduct(object selectedList)
      {
         if (selectedList is null) return;

         var selectedProduct = selectedList as Product;

         if (MessageBox.Show($"{selectedProduct.ProductName}을 삭제하시겠습니까?"
            , "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            selectedProduct.EntityState = EntityStateOption.Deleted;
            DeletedProducts.Add(selectedProduct);
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
      #endregion

      #region 다이얼로그 값 전달하는 방식 여기 완성 되면 다음 단계로 설정

      public event Action<IDialogResult> RequestClose;

      private void SaveProductListDialog(string CheckedResult)
      {        
         ButtonResult result = ButtonResult.None;
         var transportParameter = new DialogParameters();
         var CheckUpdatedProducts = Products;

         var savedResult = CheckUpdatedProducts.Where(product => product.EntityState != EntityStateOption.None);

         IEnumerable parameterValue = null;

         if ((CheckedResult?.ToLower() == "false") ||
           (CheckedResult?.ToLower() == "true") &&
           (DeletedProducts.Count > 0))
         {
            result = ButtonResult.OK;
         }

         if (CheckedResult?.ToLower() == "true" && savedResult.FirstOrDefault() is null)
         {
            MessageBox.Show("변경한 거래 목록이 없습니다.", "NG", MessageBoxButton.OK);
         }
         else if (CheckedResult?.ToLower() == "true" && MessageBox.Show($"리스트를 저장하시겠습니까?"
            , "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            result = ButtonResult.OK;
            parameterValue = savedResult;
         }

         RaiseRequestClose(result, GetDialogParameters(transportParameter, parameterValue));
      }

      private DialogParameters GetDialogParameters(DialogParameters transportParameter, IEnumerable parameterValue)
      {
         if(parameterValue != null)
         {
            foreach (var test in parameterValue)
            {
               transportParameter.Add("UpdateInformation", test);
            }
         }

         if (DeletedProducts.Count > 0)
         {
            foreach (var test in DeletedProducts)
            {
               transportParameter.Add("UpdateInformation", test);
            }
         }

         return transportParameter;
      }

      private void RaiseRequestClose(ButtonResult dialogResult, IDialogParameters dialogParameters)
         => RequestClose?.Invoke(new DialogResult(dialogResult, dialogParameters));

      public bool CanCloseDialog()
         => true;

      public void OnDialogClosed() { }
   
      public void OnDialogOpened(IDialogParameters parameters) { }
   
      #endregion

      #region 리소스

      public string Title => "제품 정보";
      public int DialogWindowWith => 1200;

      #endregion
   }
}
