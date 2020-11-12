using System;
using System.Windows.Markup;

namespace Modules.Register.EnumExtansion
{
   public class EnumBindingExtension : MarkupExtension
   {
      public Type EnumType { get; private set; }

      public EnumBindingExtension(Type enumType)
      {
         if (enumType is null || !enumType.IsEnum) throw new NullReferenceException
                 ($"{nameof(enumType)} must be of type Enums and must not be null");

         this.EnumType = enumType;
      }

      public override object ProvideValue(IServiceProvider serviceProvider)
      {
         return Enum.GetValues(EnumType);
      }
   }

   public static class ConvertToString
   {
      public static string GetLocalHeadNumber(string selectedCompanyPhoneNumber)
      {
         switch (selectedCompanyPhoneNumber)
         {
            case "Seoul":
               {
                  selectedCompanyPhoneNumber = "02";
                  break;
               }
            case "Incheon":
               {
                  selectedCompanyPhoneNumber = "032";
                  break;
               }
            case "Daejeon":
               {
                  selectedCompanyPhoneNumber = "042";
                  break;
               }
            case "Gyeonggi":
               {
                  selectedCompanyPhoneNumber = "031";
                  break;
               }
            case "Busan":
               {
                  selectedCompanyPhoneNumber = "051";
                  break;
               }
            default:
               {
                  selectedCompanyPhoneNumber = null;
                  break;
               }
         }
         return selectedCompanyPhoneNumber;
      }

      public static string GetPhoneHeadNumber(string phoneHeadNumber)
      {
         switch (phoneHeadNumber)
         {
            case "ZOZ":
               {
                  phoneHeadNumber = "010";
                  break;
               }
            case "ZOO":
               {
                  phoneHeadNumber = "011";
                  break;
               }
            case "ZOS":
               {
                  phoneHeadNumber = "017";
                  break;
               }
            case "ZON":
               {
                  phoneHeadNumber = "019";
                  break;
               }
            default:
               {
                  phoneHeadNumber = null;
                  break;
               }
         }
         return phoneHeadNumber;
      }

   }

}
