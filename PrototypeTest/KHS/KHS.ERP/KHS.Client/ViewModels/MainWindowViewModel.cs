using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KHS.Client.ViewModels
{
    public class MainWindowViewModel : ICloseWindows
    {
        private DelegateCommand _closeWindowCommand;

        public DelegateCommand CloseWindowCommand =>
            _closeWindowCommand ?? (_closeWindowCommand = new DelegateCommand(CloseWindow));

        public Action Close { get; set; }

        private void CloseWindow()
        {
            Close?.Invoke();
        }

        public bool CanClose()
        {
            return true;
        }
    }

    public interface ICloseWindows
    {
        Action Close { get; set; }

        bool CanClose(); 
    }

    public class WindowCloser
    {

        public static bool GetEnabledWindowClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledWindowClosing);
        }

        public static void SetEnabledWindowClosing(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledWindowClosing, value);
        }

        public static readonly DependencyProperty EnabledWindowClosing =
            DependencyProperty.RegisterAttached("EnabledWindowClosing", typeof(bool), typeof(WindowCloser), new PropertyMetadata(false, OnEnabledWindowClosingChanged));

        private static void OnEnabledWindowClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs dc)
        {
            if (d is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is ICloseWindows closeWindows)
                    {
                        closeWindows.Close += () =>
                        {
                            window.Close();
                        };

                        window.Closing += (sender, closeEvent) =>
                        {
                            // 이벤트를 취소해야하는 경우 true입니다. 그렇지 않으면 거짓입니다.
                            closeEvent.Cancel = !closeWindows.CanClose();
                        };
                    }
                };
            }
        }
    }

}
