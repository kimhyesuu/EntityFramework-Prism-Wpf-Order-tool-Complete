using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS.ERP.Business.Converter
{
   public static class PhoneNumberConverter
   {
      public static string ConvertToNumber(string number)
      {
         string stringConvert = string.Empty;

         switch(number)
         {
            case "ZOZ":
               {
                  stringConvert = "010";
               }
               break;
            case "ZOO":
               {
                  stringConvert = "011";
               }
               break;
            case "ZOS":
               {
                  stringConvert = "017";
               }
               break;
            case "ZON":
               {
                  stringConvert = "019";
               }
               break;
         }

         return stringConvert;
      }

      public static string ConvertToString(string number)
      {
         string stringConvert = string.Empty;

         switch (number)
         {
            case "010":
               {
                  stringConvert = "ZOZ";
               }
               break;
            case "011":
               {
                  stringConvert = "ZOO";
               }
               break;
            case "017":
               {
                  stringConvert = "ZOS";
               }
               break;
            case "019":
               {
                  stringConvert = "ZON";
               }
               break;
         }

         return stringConvert;
      }
   }
}
