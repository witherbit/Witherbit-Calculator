using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;

namespace WhiteByte.Utils
{
    internal static class UiExtensions
    {
        #region Window
        enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }
        enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19
            // ...
        }
        [StructLayout(LayoutKind.Sequential)]
        struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }
        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        internal static void EnableBlur(this Window window)
        {
            var windowHelper = new WindowInteropHelper(window);

            var accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
            };

            int accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        #endregion

        #region ContentControl

        public static void OpacityAnimation(this ContentControl instance, double startpoint, double endpoint, double duration)
        {
            DoubleAnimation da = new DoubleAnimation(startpoint, endpoint, TimeSpan.FromSeconds(duration));
            instance.BeginAnimation(ContentControl.OpacityProperty, da);
        }
        public static void MarginAnimation(this TextBlock grid, Thickness thickness, double duration)
        {
            grid.BeginAnimation(TextBlock.MarginProperty, new ThicknessAnimation(thickness, TimeSpan.FromSeconds(duration)));
        }
        public static void ForegroundFadeTo(this TextBlock border, Brush to, double duration)
        {
            ColorAnimation colorChangeAnimation = new ColorAnimation();
            colorChangeAnimation.From = ((SolidColorBrush)border.Foreground).Color;
            colorChangeAnimation.To = ((SolidColorBrush)to).Color;
            colorChangeAnimation.Duration = TimeSpan.FromSeconds(duration);

            PropertyPath colorTargetPath = new PropertyPath("(TextBox.Foreground).(SolidColorBrush.Color)");
            Storyboard CellBackgroundChangeStory = new Storyboard();
            Storyboard.SetTarget(colorChangeAnimation, border);
            Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
            CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
            CellBackgroundChangeStory.Begin();
        }
        public static void FontSizeAnimation(this TextBlock grid, double to, double duration)
        {
            grid.BeginAnimation(TextBlock.FontSizeProperty, new DoubleAnimation(to, TimeSpan.FromSeconds(duration)));
        }
        public static void BackgroundFadeTo(this Border border, Brush to, double duration)
        {
            ColorAnimation colorChangeAnimation = new ColorAnimation();
            colorChangeAnimation.From = ((SolidColorBrush)border.Background).Color;
            colorChangeAnimation.To = ((SolidColorBrush)to).Color;
            colorChangeAnimation.Duration = TimeSpan.FromSeconds(duration);

            PropertyPath colorTargetPath = new PropertyPath("(Panel.Background).(SolidColorBrush.Color)");
            Storyboard CellBackgroundChangeStory = new Storyboard();
            Storyboard.SetTarget(colorChangeAnimation, border);
            Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
            CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
            CellBackgroundChangeStory.Begin();
        }
        public static void MarginAnimation(this Border border, Thickness thickness, double duration)
        {
            border.BeginAnimation(Border.MarginProperty, new ThicknessAnimation(thickness, TimeSpan.FromSeconds(0.1)));
        }
        public static void HeightAnimation(this Control grid, double startpoint, double endpoint, double duration)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = startpoint;
            da.To = endpoint;
            da.Duration = TimeSpan.FromSeconds(duration);
            grid.BeginAnimation(Control.HeightProperty, da);
        }

        #endregion
    }
}
