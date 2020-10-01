using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSUiModel.Enum.PageControl
{
   public enum IndexPages
   {
      ProcessManagement,
      ProductionManagement,      
   }

   public class ChangingPage
   {
      public IndexPages CurrentView { get; set; }
   }
}
