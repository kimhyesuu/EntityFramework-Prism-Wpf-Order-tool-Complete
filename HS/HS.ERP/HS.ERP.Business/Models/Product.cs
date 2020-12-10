namespace HS.ERP.Business.Models
{
   using HS.ERP.Business.Models.Enums;

   public class Product 
   {
      public Product() { }
   
      public Product(long? id)
         => this.ProductId = id;

      public long? ProductId { get; set; }
      public string ProductName { get; set; } 
      public string ProductPrice { get; set; }
      public string Series { get; set; }
      public string CoreProcessor { get; set; }
      public string CoreSize { get; set; }
      public string Connectivity { get; set; }
      public string Speed { get; set; }
      public string NumberOfIO { get; set; }
      public string Peripherals { get; set; }
      public string ProgramMemoryType { get; set; }
      public string ProgramMemorySize { get; set; }
      public string RamSize { get; set; }
      public string EEPROMSize { get; set; }
      public string DataConverter { get; set; }
      public string VoltageSupply { get; set; }
      public string OperatingTemperature { get; set; }
      public string OscillatorType { get; set; }
      public string PakageCase { get; set; }
      public string CreatedDate { get; set; }
      public string UpdatedDate { get; set; }
      public EntityStateOption EntityState { get; set; }


   }
}
