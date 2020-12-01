namespace HS.ERP.Business.Converter
{
   using System;
   using System.ComponentModel;
   using System.Globalization;
   using System.Reflection;

   public class EnumDescriptionTypeConverter : EnumConverter
   {
      public EnumDescriptionTypeConverter(Type type)
          : base(type)
      {
      }

      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
      {
         if (destinationType == typeof(string))
         {
            if (value != null)
            {
               FieldInfo info = value.GetType().GetField(value.ToString());
               if (info != null)
               {
                  var attributes =
                      (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

                  return (attributes.Length > 0 && !string.IsNullOrWhiteSpace(attributes[0].Description)
                      ? attributes[0].Description : value.ToString());
               }
            }
         }
         return base.ConvertTo(context, culture, value, destinationType);
      }
   }
}
