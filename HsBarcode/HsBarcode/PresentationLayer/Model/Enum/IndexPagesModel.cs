using HsBarcode.PresentationLayer.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HsBarcode.PresentationLayer.Model.Enum
{
   public enum IndexPages
   {
      SelectIssueAndReceipt = 0,
      GoodIssuePage = 1,
      GoodReceiptPage = 2
   }

   public class IndexPagesModel 
   {  
      public IndexPages CurrentView { get; set; }     
   }
}
