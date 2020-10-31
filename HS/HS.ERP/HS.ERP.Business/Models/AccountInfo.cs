using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace HS.ERP.Business.Models
{
   public class AccountInfo 
   {
      // seq
      public ObservableCollection<AccountInfo> AccountList { get; set; }
      public int? AccountId { get; set; }
      public string CompanyName { get; set; }
      public string CompanyManager { get; set; }
      public string PhoneNumber { get; set; }
      public string CompanyEmail { get; set; }
      public string Address { get; set; }
      public string Description { get; set; }
      public DateTime CreatedDate { get; set; }
      public DateTime UpdateTime { get; set; }


      #region Account Property 
      //private int? _id;
      //private string _companyName;
      //private string _companyManager;
      //private string _phoneNumber;
      //private string _email;
      //private string _address;
      //private string _description;
      //private DateTime _createdDate;
      //private DateTime _updatedDate;


      //public int? AccountId
      //{
      //   get { return _id; }
      //   set { SetProperty(ref _id, value); }
      //}

      //public string CompanyName
      //{
      //   get { return _companyName; }
      //   set { SetProperty(ref _companyName, value); }
      //}

      //public string CompanyManager
      //{
      //   get { return _companyManager; }
      //   set { SetProperty(ref _companyManager, value); }
      //}

      //public string PhoneNumber
      //{
      //   get { return _phoneNumber; }
      //   set { SetProperty(ref _phoneNumber, value); }
      //}

      //public string CompanyEmail
      //{
      //   get { return _email; }
      //   set { SetProperty(ref _email, value); }
      //}

      //public string Address
      //{
      //   get { return _address; }
      //   set { SetProperty(ref _address, value); }
      //}

      //public string Description
      //{
      //   get { return _description; }
      //   set { SetProperty(ref _description, value); }
      //}

      //public DateTime CreatedDate
      //{
      //   get { return _createdDate; }
      //   set { SetProperty(ref _createdDate, value); }
      //}

      //public DateTime UpdateTime
      //{
      //   get { return _updatedDate; }
      //   set { SetProperty(ref _updatedDate, value); }
      //}

      #endregion

   }
}
