using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HS.ERP.Outlook.Core.Dependency
{
    #region 내가 부족한 것
    //1.public static readonly DependencyProperty EnabledWindowDragMove =
    //        DependencyProperty.RegisterAttached("EnabledWindowDragMove", typeof(ICommand),
    //            typeof(WindowDragMover), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnEnabledWindowDragMoveChanged)));
    // 이 구문에서 new FrameworkPropertyMetadata(new PropertyChangedCallback(OnEnabledWindowDragMoveChanged))이것이 이해가 가지 않는다.
    //
    #endregion

    #region Application을 움직일 때 사용
    public class WindowDragMover
    {
        public static ICommand GetEnabledWindowDragMove(UIElement obj)
        {
            return (ICommand)obj.GetValue(EnabledWindowDragMove);
        }

        public static void SetEnabledWindowDragMove(UIElement obj, ICommand value)
        {
            obj.SetValue(EnabledWindowDragMove, value);
        }

       
        public static readonly DependencyProperty EnabledWindowDragMove =
            DependencyProperty.RegisterAttached("EnabledWindowDragMove", typeof(ICommand), 
                typeof(WindowDragMover), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnEnabledWindowDragMoveChanged)));

        private static void OnEnabledWindowDragMoveChanged(DependencyObject d, DependencyPropertyChangedEventArgs dc)
        {
            if (d is Window window)
            {
                window.MouseLeftButtonDown += (s, e) =>
                {
                    if (window.DataContext is IWindowResources vm)
                    {
                        vm.WindowDragMove += () =>
                        {
                            window.DragMove();
                        };

                        window.MouseLeftButtonDown += (sender, dragMoveEvent) =>
                        {                        
                            if (dragMoveEvent.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                            {
                                window.DragMove();
                            } 
                        };
                    }
                };
            }
        }
    }
    #endregion

    #region Application을 닫을 때 사용
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
                    if (window.DataContext is IWindowResources vm)
                    {
                        vm.WindowClose += () =>
                        {
                            window.Close();
                        };

                        window.Closing += (sender, closeEvent) =>
                        {
                            // 이벤트를 취소해야하는 경우 true입니다. 그렇지 않으면 거짓입니다.
                            closeEvent.Cancel = !vm.WindowCanClose();
                        };
                    }
                };
            }
        }
    }
    #endregion
}
