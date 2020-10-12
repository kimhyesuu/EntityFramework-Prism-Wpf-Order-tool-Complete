using KHS.Client.Views;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KHS.Client.Common
{
    public class BootStrapper : UnityBootstrapper
    {       
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<MainWindowView>();
        }
 
        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = Shell as MainWindowView;
            Application.Current.MainWindow.Show();
        }
    }
}
