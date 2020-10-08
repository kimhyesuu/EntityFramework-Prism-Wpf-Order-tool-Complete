using HS.ERP.ViewModel.ViewModel;
using System.Windows;

namespace HS.ERP.Application
{
   /// <summary>
   /// App.xaml에 대한 상호 작용 논리
   /// </summary>
   public partial class App 
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);
         View.MainWindowView view = new View.MainWindowView();
         view.DataContext = new MainViewModel();
         view.Show();
      }
   }
}
