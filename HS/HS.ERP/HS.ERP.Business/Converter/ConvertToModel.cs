using HS.ERP.Business.Models;
using HS.ERP.DataAccess.Domain;
using System.Collections.Generic;
using System.Linq;

namespace HS.ERP.Business.Converter
{
   public static class ConvertToModel
   {
      internal static DAccount ClientToDomain(Account info)
      {
         var dAccount = new DAccount();

         dAccount.AccountId = info.AccountId;
         dAccount.CompanyName = info.CompanyName;
         dAccount.CompanyEmail = info.CompanyEmail;
         dAccount.Address = info.Address;
         dAccount.ContactName = info.ContactName;
         dAccount.Department = info.Department;
         dAccount.Position = info.Position;
         dAccount.TelePrefix = info.TelePrefix;
         dAccount.TelePhoneNumber = info.TelePhoneNumber;
         dAccount.Description = info.Description;
         dAccount.CreatedDate = info.CreatedDate;
         dAccount.UpdatedDate = info.UpdatedDate is null ? null : info.UpdatedDate;

         return dAccount;
      }


      internal static IEnumerable<Account> DomainToClient(IEnumerable<DAccount> infos)
      {
         var list = new List<Account>();

         foreach (var info in infos)
         {
            list.Add(new Account
            {
               AccountId = info.AccountId,
               CompanyName = info.CompanyName,
               CompanyEmail = info.CompanyEmail,
               Address = info.Address,
               ContactName = info.ContactName,
               Department = info.Department,
               Position = info.Position,
               TelePrefix = info.TelePrefix,
               TelePhoneNumber = info.TelePhoneNumber,
               FullPhoneNumber = info.TelePrefix + info.TelePhoneNumber,
               Description = info.Description,
               CreatedDate = info.CreatedDate,
               UpdatedDate = info.UpdatedDate is null ? null : info.UpdatedDate,
               EntityState = Models.Enums.EntityStateOption.DBUpdated              
            });
         }

         return list;
      }

      internal static DProduct ClientToDomain(Product info)
      {
         var domainProduct = new DProduct();

         domainProduct.ProductId = info.ProductId;
         domainProduct.ProductName = info.ProductName;
         domainProduct.ProductPrice = int.Parse(info.ProductPrice);
         domainProduct.CreatedDate = info.CreatedDate;
         domainProduct.UpdatedDate = info.UpdatedDate;
         return domainProduct;
      }

      internal static DProductSpec SpecClientToDomain(Product info)
      {
         var domainProductSpec = new DProductSpec();

         domainProductSpec.ProductIdFK = info.ProductId;
         domainProductSpec.Series = info.Series;
         domainProductSpec.CoreProcessor = info.CoreProcessor;
         domainProductSpec.CoreSize = info.CoreSize;
         domainProductSpec.Connectivity = info.Connectivity;
         domainProductSpec.Speed = info.Speed;
         domainProductSpec.NumberOfIO = info.NumberOfIO;
         domainProductSpec.Peripherals = info.Peripherals;
         domainProductSpec.ProgramMemoryType = info.ProgramMemoryType;
         domainProductSpec.ProgramMemorySize = info.ProgramMemorySize;
         domainProductSpec.RamSize = info.RamSize;
         domainProductSpec.EEPROMSize = info.EEPROMSize;
         domainProductSpec.DataConverter = info.DataConverter;
         domainProductSpec.VoltageSupply = info.VoltageSupply;
         domainProductSpec.OperatingTemperature = info.OperatingTemperature;
         domainProductSpec.OscillatorType = info.OscillatorType;
         domainProductSpec.PakageCase = info.PakageCase;

         return domainProductSpec;
      }

      internal static IEnumerable<Product> DomainToClient(IEnumerable<DProduct> products, IEnumerable<DProductSpec> specs)
      {   
         var query = from productInfo in products
                     join spec in specs on productInfo.ProductId equals spec.ProductIdFK
                     select new Product
                     {
                        ProductId = productInfo.ProductId,
                        ProductName = productInfo.ProductName,
                        ProductPrice = productInfo.ProductPrice.ToString(),
                        CreatedDate = productInfo.CreatedDate,
                        UpdatedDate = productInfo.UpdatedDate,
                        Series = spec.Series,
                        CoreProcessor = spec.CoreProcessor,
                        CoreSize = spec.CoreSize,
                        Connectivity = spec.Connectivity,
                        Speed = spec.Speed,
                        NumberOfIO = spec.NumberOfIO,
                        Peripherals = spec.Peripherals,
                        ProgramMemoryType = spec.ProgramMemoryType,
                        ProgramMemorySize = spec.ProgramMemorySize,
                        RamSize = spec.RamSize,
                        EEPROMSize = spec.EEPROMSize,
                        DataConverter = spec.DataConverter,
                        VoltageSupply = spec.VoltageSupply,
                        OperatingTemperature = spec.OperatingTemperature,
                        OscillatorType = spec.OscillatorType,
                        PakageCase = spec.PakageCase,
                        
                        EntityState = Models.Enums.EntityStateOption.DBUpdated
                     };

         return query;
      }

      internal static DOrder ClientToDomain(Ordered info)
      {
         var dOrder = new DOrder()
         {
            OrderId = info.OrderId,
            OrderPrice = info.OrderPrice,
            OrderQuantity = info.OrderQuantity,
            Description = info.Description,
            CreatedDate = info.CreatedDate,
            AccountIdFK = info.AccountId
         };

         return dOrder;
      }

      internal static DOrderProduct OrderDetailClientToDomain(Ordered info)
      {
         var dOrderProduct = new DOrderProduct()
         {
            ProductName = info.ProductName,
            TotalQuantity = info.TotalQuantity,
            OrderIdFK = info.OrderId,
            ProductIdFK = info.ProductId
         };

         return dOrderProduct;
      }

      internal static IEnumerable<Ordered> DomainToClient(IEnumerable<DOrder> orders, IEnumerable<DOrderProduct> orderProducts, IEnumerable<DAccount> accounts)
      {
         int seq = 1;
         var query = from orderInfo in orders
                     join detailInfo in orderProducts on orderInfo.OrderId equals detailInfo.OrderIdFK
                     join accountInfo in accounts on orderInfo.AccountIdFK equals accountInfo.AccountId
                     select new Ordered(orderInfo.OrderId)
                     {
                        SequentialNumber = seq++,
                        ProductId = detailInfo.ProductIdFK,
                        AccountId = orderInfo.AccountIdFK,
                        ProductName = detailInfo.ProductName,
                        OrderPrice = orderInfo.OrderPrice,
                        OrderQuantity = orderInfo.OrderQuantity,
                        TotalQuantity = detailInfo.TotalQuantity,
                        CompanyName = accountInfo.CompanyName,
                        ContactName = accountInfo.ContactName,
                        FullPhoneNumber = accountInfo.TelePrefix + accountInfo.TelePhoneNumber,
                        CreatedDate = orderInfo.CreatedDate,
                        Description = orderInfo.Description,
                        EntityState = Models.Enums.EntityStateOption.DBUpdated
                     };

         return query;
      }  
   }
}
