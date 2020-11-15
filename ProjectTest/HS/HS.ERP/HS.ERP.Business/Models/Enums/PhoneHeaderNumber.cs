namespace HS.ERP.Business.Models.Enums
{
   using HS.ERP.Business.Converter;
   using System.ComponentModel;

   [TypeConverter(typeof(EnumDescriptionTypeConverter))]
   public enum PhoneHeaderNumber
   {
      //ZeroOneZero
      [Description("010")]
      ZOZ,

      //ZeroOneOne
      [Description("011")]
      ZOO,

      //ZeroOneSeven
      [Description("017")]
      ZOS,

      //ZeroOneNine
      [Description("019")]
      ZON
   }

   [TypeConverter(typeof(EnumDescriptionTypeConverter))]
   public enum LocalNumber
   {
      [Description("02")]
      Seoul,

      [Description("032")]
      Incheon,

      [Description("042")]
      Daejeon,

      [Description("031")]
      Gyeonggi,

      [Description("051")]
      Busan
   }

  
}
