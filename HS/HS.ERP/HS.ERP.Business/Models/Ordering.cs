namespace HS.ERP.Business.Models
{
   using System.Collections.ObjectModel;

   public class Ordering : ObservableCollection<Ordered>
   {
      public Ordering(Ordered ordered)
      {
         
      }
   }       
}
