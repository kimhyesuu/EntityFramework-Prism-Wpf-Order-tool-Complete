using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using HS.ERP.Business.Managers;
using HS.ERP.Business.Models;
using HS.ERP.Business.Models.Enums;
using HS.ERP.Business.Services;
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

      private IRepogitoryManager<Product> RepogitoryManager { get; set; }
      private IDataService<Product> DataService { get; set; }

      #region 프로퍼티 
      public ObservableCollection<Product> Products
      {
         get { return _productList; }
         set { SetProperty(ref _productList, value); }
      }

      public Product SelectedProduct
      {
         get { return _productInfo; }
         set { SetProperty(ref _productInfo, value); }
      }

      public List<Product> InnerProducts { get; set; }

      public bool CanSaveUpdatedState { get; set; }

      public bool CanSaveExcute
      {
         get => true;
         set => MoveProductInfoToListCommand.RaiseCanExecuteChanged();
      }

      public string ProductTitleHeader => "삭제";

      #endregion

      #region Constructor

      public ProductInfoViewModel()
      {
         DataInitialize();
         CommandInitialize();
      }

      #endregion

      public DelegateCommand<string> MoveProductInfoToListCommand { get; private set; }
      public DelegateCommand<object> SelectedProductInfoCommand { get; private set; }
      public DelegateCommand<string> SaveProductListCommand { get; private set; }

      #region 제품 CRUD
      private void DeleteProduct(object selectedList)
      {
         //여기서 오더의 유효성 판단 
         if (selectedList is null) return;

         var selectedProduct = selectedList as Product;

         if (MessageBox.Show($"{selectedProduct.ProductName}을 삭제하시겠습니까?"
            , "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            selectedProduct.EntityState = EntityStateOption.Deleted;
            InnerProducts.Add(selectedProduct);
            Products.Remove(selectedProduct);
         }
      }

      private void AddOrUpdate(string product)
      {
         var receivedInfo = ConvertStringToProductInfo(product);

         if (IsAdd(receivedInfo))
         {
            AddProductInfo(receivedInfo);
         }
         else
         {
            if (IsStoredInfoFromDB(receivedInfo) is false)
            {
               CanSaveUpdatedState = false;
            }

            UpdateProductInfo(receivedInfo);
         }
      }

      private bool IsStoredInfoFromDB(Product receivedInfo)
      {
         var result = RepogitoryManager.GetAll();

         if(result is null)
         {
            return false;
         }

         foreach(var info in result)
         {
            if(receivedInfo.ProductId == info.ProductId)
            {
               return true;
            }
         }

         return false;
      }

      private Product ConvertStringToProductInfo(string product)
      {
         string[] productInfo = product.Split(':');

         var receivedInfo = new Product
         {
            ProductId = SelectedProduct.ProductId,
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
            EntityState = SelectedProduct.EntityState,
            CreatedDate = SelectedProduct.CreatedDate is null ? DateTime.Now.ToString("yyyy-MM-dd") : SelectedProduct.CreatedDate
         };

         return receivedInfo;
      }

      private void UpdateProductInfo(Product receivedInfo)
      {
         if(CanSaveUpdatedState)
         {
            receivedInfo.EntityState = EntityStateOption.Updated;
         }
         else
         {
            receivedInfo.EntityState = EntityStateOption.Inserted;
         }

         receivedInfo.UpdatedDate = DateTime.Now.ToString("yyyy-MM-dd");
         Products.Insert(Products.IndexOf(SelectedProduct), receivedInfo);
         Products.Remove(SelectedProduct);

         receivedInfo = null;
         CanSaveUpdatedState = true;
         ProductInfoInit();
      }

      private void AddProductInfo(Product receivedInfo)
      {
         receivedInfo.EntityState = EntityStateOption.Inserted;
         Products.Add(receivedInfo);

         receivedInfo = null;
         ProductInfoInit();
      }

      private bool IsAdd(Product receivedInfo)
      => receivedInfo.EntityState is EntityStateOption.None;

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

         var savedResult = ProductListToSave(Products);

         IEnumerable parameterValue = null;

         if (CheckedResult?.ToLower() == "true" &&
            savedResult.FirstOrDefault() != null &&
            MessageBox.Show($"리스트를 저장하시겠습니까?"
            , "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            result = ButtonResult.OK;
            parameterValue = savedResult;
            SaveDb(savedResult);
         }
         else if (CheckedResult?.ToLower() == "false"
          && savedResult.FirstOrDefault() != null
          )
         {
            if (MessageBox.Show($"저장되지 않은 정보가 있습니다.\n저장하시겠습니까?", "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
               result = ButtonResult.OK;
               parameterValue = savedResult;
               SaveDb(savedResult);
            }
         }

         RaiseRequestClose(result, GetDialogParameters(transportParameter, parameterValue));
         Products = null;
         InnerProducts = null;
         SelectedProduct = null;
      }

      private void SaveDb(IEnumerable<Product> savedResult)
      {
         DataService.SendEntityStatus(savedResult);
      }

      private IEnumerable<Product> ProductListToSave(ObservableCollection<Product> products)
      {
         foreach (var item in products.Where(product => product.EntityState != EntityStateOption.DBUpdated))
         {
            InnerProducts.Add(item);
         }

         return InnerProducts.AsEnumerable();
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

         return transportParameter;
      }

      private void RaiseRequestClose(ButtonResult dialogResult, IDialogParameters dialogParameters)
         => RequestClose?.Invoke(new DialogResult(dialogResult, dialogParameters));

      public bool CanCloseDialog()
         => true;

      public void OnDialogClosed() { }
   
      public void OnDialogOpened(IDialogParameters parameters) { }

      #endregion

      private void CommandInitialize()
      {
         SaveProductListCommand = new DelegateCommand<string>(SaveProductListDialog);
         MoveProductInfoToListCommand = new DelegateCommand<string>(AddOrUpdate).ObservesCanExecute(() => CanSaveExcute);
         SelectedProductInfoCommand = new DelegateCommand<object>(DeleteProduct);
      }

      private void DataInitialize()
      {
         RepogitoryManager = new ProductManager();
         InnerProducts = new List<Product>();
         DataService = new ProductService();
         CanSaveUpdatedState = true;

         var result = RepogitoryManager.GetAll();

         Products = result != null ? new ObservableCollection<Product>(result) : new ObservableCollection<Product>();

         ProductInfoInit();
      }

      private void ProductInfoInit()
      {
         SelectedProduct = null;
         SelectedProduct = new Product(Newid());
         SelectedProduct.EntityState = EntityStateOption.None;
      }

      #region 리소스

      public string Title => "제품 정보";
      public int DialogWindowWith => 1200;

      #endregion
   }
}
