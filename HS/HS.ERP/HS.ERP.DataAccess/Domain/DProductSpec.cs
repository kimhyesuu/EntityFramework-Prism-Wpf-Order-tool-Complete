using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HS.ERP.DataAccess.Domain
{
   [Table("ProductSpec")]
   public class DProductSpec
   {
      [Key]
      [Column("ProductSpecId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public long? ProductSpecId { get; set; }

      [Column("Series")]
      public string Series { get; set; }

      [Column("CoreProcessor")]
      public string CoreProcessor { get; set; }

      [Column("CoreSize")]
      public string CoreSize { get; set; }

      [Column("Connectivity")]
      public string Connectivity { get; set; }

      [Column("Speed")]
      public string Speed { get; set; }

      [Column("NumberOfIO")]
      public string NumberOfIO { get; set; }

      [Column("Peripherals")]
      public string Peripherals { get; set; }

      [Column("ProgramMemoryType")]
      public string ProgramMemoryType { get; set; }

      [Column("ProgramMemorySize")]
      public string ProgramMemorySize { get; set; }

      [Column("RamSize")]
      public string RamSize { get; set; }

      [Column("EEPROMSize")]
      public string EEPROMSize { get; set; }

      [Column("DataConverter")]
      public string DataConverter { get; set; }

      [Column("VoltageSupply")]
      public string VoltageSupply { get; set; }

      [Column("OperatingTemperature")]
      public string OperatingTemperature { get; set; }

      [Column("OscillatorType")]
      public string OscillatorType { get; set; }

      [Column("PakageCase")]
      public string PakageCase { get; set; }

      public long? ProductIdFK { get; set; }
      public virtual DProduct DProduct { get; set; }
   }
}
