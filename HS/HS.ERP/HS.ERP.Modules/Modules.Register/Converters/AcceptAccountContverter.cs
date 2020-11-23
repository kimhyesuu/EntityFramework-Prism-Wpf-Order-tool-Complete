using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Modules.Register.Converters
{
   public class AcceptAccountContverter : IMultiValueConverter
   {
      public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
      {
         var accountinfo = values.OfType<string>().ToArray();
         var sb = new StringBuilder();

         sb.Append(string.Join(":", accountinfo));

         return sb.ToString();
      }

      public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}
