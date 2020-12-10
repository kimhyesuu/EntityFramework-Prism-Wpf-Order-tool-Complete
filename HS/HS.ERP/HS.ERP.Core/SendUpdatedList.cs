namespace HS.ERP.Core
{
   using Prism.Events;
   using System.Collections.Generic;

   public class SendUpdatedList : PubSubEvent<IEnumerable<object>> { }
}
