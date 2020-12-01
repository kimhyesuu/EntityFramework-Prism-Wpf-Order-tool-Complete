using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Modules.Register
{
   public class AcceptTextAccountInfoBehavior
   {
      public static int GetMyProperty(DependencyObject obj)
      {
         return (int)obj.GetValue(MyPropertyProperty);
      }

      public static void SetMyProperty(DependencyObject obj, int value)
      {
         obj.SetValue(MyPropertyProperty, value);
      }

      // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty MyPropertyProperty =
          DependencyProperty.RegisterAttached("MyProperty", typeof(int), typeof(AcceptTextAccountInfoBehavior), new PropertyMetadata(0));
   }
}
