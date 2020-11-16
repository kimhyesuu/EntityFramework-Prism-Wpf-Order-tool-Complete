using System;

namespace HS.ERP.Core.Dependency
{
   public interface IWindowResources
   {
      // window DrageMove
      Action WindowDragMove { get; set; }

      // Window Close
      Action WindowClose { get; set; }
      bool WindowCanClose();
   }
}
