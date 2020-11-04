using Prism.Mvvm;
using System;

namespace HS.ERP.Business.Models
{
   public class ProductInfo : BindableBase
   {
      public int? ProductId { get; set; }
      public string ProductName { get; set; }
      public string Description { get; set; }
      public DateTime CreatedTime { get; set; }
      public DateTime UpdatedTime { get; set; }

      #region bindableBase를 써도 되는 것인가를 확인해보자
      //private int? _productId;
      //private string _productName;
      //private string _description;
      //private DateTime _createdTime;
      //private DateTime _updatedTime;

      //#region Product Property 

      //public int? ProductId
      //{
      //   get { return _productId; }
      //   set { SetProperty(ref _productId, value); }
      //}


      //public string ProductName
      //{
      //   get { return _productName; }
      //   set { SetProperty(ref _productName, value); }
      //}

      //public string Description
      //{
      //   get { return _description; }
      //   set { SetProperty(ref _description, value); }
      //}

      //public DateTime CreatedTime
      //{
      //   get { return _createdTime; }
      //   set { SetProperty(ref _createdTime, value); }
      //}

      //public DateTime UpdatedTime
      //{
      //   get { return _updatedTime; }
      //   set { SetProperty(ref _updatedTime, value); }
      //}

      //#endregion
      #endregion

   }
}
