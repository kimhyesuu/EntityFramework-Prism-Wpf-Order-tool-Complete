using HsBarcode.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HsBarcode.PresentationLayer.ViewModel.Common
{
   public class MainPageClickContentVeiwModel
   {
      public MainPageClickContentVeiwModel()
      {
         //ShowTheMenuCommand = new RelayCommand(o => OnShowTheMenu((Window)o));
         CloseCommand = new RelayCommand(o => OnWindowClose((Window)o));
      }

     // public ICommand ShowTheMenuCommand { set; private get; }
      public ICommand CloseCommand { set; private get; }

      private void OnWindowClose(Window window)
      {
         if (window != null)
            window.Close();
      }

      //private void OnShowTheMenu(Window window)
      //{
      //   ContextMenu cm = window.FindResource("cmButton") as ContextMenu;
      //   cm.PlacementTarget = window. as Button;
      //   cm.IsOpen = true;
      //}
   }
}
