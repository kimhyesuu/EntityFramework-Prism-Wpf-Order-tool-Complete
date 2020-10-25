using Prism.Services.Dialogs;
using System.Windows;

namespace HS.ERP.Outlook.Core.Dialogs
{
    public partial class RegistryDialogWindow : Window, IDialogWindow
    {
        public RegistryDialogWindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}
