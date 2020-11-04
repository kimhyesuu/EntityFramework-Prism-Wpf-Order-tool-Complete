using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSUiModel.Enum.PageControl
{
   public enum IndexPages
   {
      Main,
      MaterialManagement,
      ProcessManagement,
      ProductionManagement,      
      StandardInformation
   }

   public class ChangingPage
   {
      public IndexPages CurrentView { get; set; }
   }
}
