using System;
using System.Windows.Markup;

namespace HS.ERP.Core.Extansions
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
}
