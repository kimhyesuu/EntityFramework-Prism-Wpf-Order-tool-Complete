using HS.ERP.Core;
using HS.ERP.Core.Dependency;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace Modules.Menu.ViewModels
{
   public class MainHeadBarViewModel : BindableBase, IWindowResources
   {
      private IDialogService DialogService { get; }
      private IRegionManager RegionManager { get; }

      public Action WindowClose { get; set; }
      public Action WindowDragMove { get; set; }

      public MainHeadBarViewModel(IDialogService dialogService, IRegionManager regionManager)
      {
         this.DialogService = dialogService;
         this.RegionManager = regionManager;
         WindowCloseCommand = new DelegateCommand(OnClose);
         DragMoveCommand = new DelegateCommand(OnDrag);
         OpenTheRegisterWindowCommand = new DelegateCommand<object>(o => ShowPopup(o));
      }

      public DelegateCommand WindowCloseCommand { get; private set; }

      public DelegateCommand DragMoveCommand { get; private set; }

      public DelegateCommand<object> OpenTheRegisterWindowCommand { get; private set; }


      public bool WindowCanClose() => true;

      private void OnClose() => WindowClose?.Invoke();

      private void OnDrag() => WindowDragMove?.Invoke();


      private void ShowPopup(object navigationPopupPath)
      {
         if (navigationPopupPath is null) return;
         var para = navigationPopupPath as FrameworkElement;

         DialogService.Show(para.Name, null, r =>
         {
            if (IsNullOrParameter(r.Parameters))
            {
               //아직
            }
            else
            {
               //아직
            }

         });
      }

      private void OpenSelectedPopWindow(FrameworkElement para)
      {




      }

      private bool IsNullOrParameter(IDialogParameters parameters)
      {
         return parameters.GetValue<string>("submessage") != string.Empty;
      }

   }
}
