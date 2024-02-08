using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WhiteByte.Utils
{
    internal static class Extensions
    {
        public static object CSharpScript { get; private set; }

        public static SolidColorBrush GetBrush(this string hex)
        {
            if (hex[0] != '#') hex = hex.Insert(0, "#");
            return (SolidColorBrush)new BrushConverter().ConvertFrom(hex);
        }
        public static Color GetColor(this string hex)
        {
            if (hex[0] != '#') hex = hex.Insert(0, "#");
            return (Color)ColorConverter.ConvertFromString(hex);
        }
        public static void Invoke(this ContentControl instanse, Action action)
        {
            instanse.Dispatcher?.BeginInvoke(DispatcherPriority.Background, action);
        }
        public static void Invoke(this Page instanse, Action action)
        {
            instanse.Dispatcher?.BeginInvoke(DispatcherPriority.Background, action);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string[] GetSplitByIndex(this string str, params int[] index)
        {
            string[] result = new string[index.Length];
            var spt = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < index.Length; i++)
            {
                result[i] = spt[index[i]];
            }
            return result;
        }

        public static void TryFocus(this NumberControl control)
        {
            control.SetFocus();
        }
    }
}
