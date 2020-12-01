using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Modules.Register.Converters
{
   public class AcceptInfoContverter : IMultiValueConverter
   {
      public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
      {
         var inFo = values.OfType<string>().ToArray();
         var sb = new StringBuilder();

         sb.Append(string.Join(":", inFo));

         return sb.ToString();
      }

      public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}
