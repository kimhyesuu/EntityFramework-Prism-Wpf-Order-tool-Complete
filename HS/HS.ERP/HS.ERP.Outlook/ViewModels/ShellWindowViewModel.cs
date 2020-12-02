using HS.ERP.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace HS.ERP.Outlook.ViewModels
{
   public class ShellWindowViewModel : BindableBase, IWindowResources
   {
      private IDialogService DialogService { get; }
      private IRegionManager RegionManager { get; }
      private IEventAggregator EventAggregator { get; }

      public Action WindowClose { get; set; }
      public Action WindowDragMove { get; set; }

      public ShellWindowViewModel(IDialogService dialogService,
         IRegionManager regionManager, IEventAggregator eventAggregator)
      {
         this.DialogService = dialogService;
         this.RegionManager = regionManager;
         this.EventAggregator = eventAggregator;
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

         DialogService.ShowDialog(para.Name, null, r =>
         {
            if (IsNullOrParameter(r.Parameters) && r.Result is ButtonResult.OK)
            {
               EventAggregator.GetEvent<SendUpdatedList>().Publish(r.Parameters.GetValues<object>("UpdateInformation"));
            }
         });
      }

      private bool IsNullOrParameter(IDialogParameters parameters)
        => parameters.GetValues<object>("UpdateInformation") != null;     
   }
}
