using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HS.ERP.Core
{
   public class SendUpdatedList : PubSubEvent<IEnumerable<object>> { }
}
