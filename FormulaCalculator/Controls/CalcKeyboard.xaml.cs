using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhiteByte.Utils;

namespace FormulaCalculator.Controls
{
    /// <summary>
    /// Логика взаимодействия для CalcKeyboard.xaml
    /// </summary>
    public partial class CalcKeyboard : UserControl
    {
        public event EventHandler<string> OnClick;
        public CalcKeyboard()
        {
            InitializeComponent();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Border;
            var name = button.Name.Replace("uiButton", "");
            switch (name)
            {
                case "Br1":
                    button.Background = "#ffb397".GetBrush();
                    break;
                case "Br2":
                    button.Background = "#ffb397".GetBrush();
                    break;
                case "MinusX":
                    button.Background = "#ffb397".GetBrush();
                    break;

                case "NSqrt":
                    button.Background = "#9cffe7".GetBrush();
                    break;
                case "Sqrt":
                    button.Background = "#9cffe7".GetBrush();
                    break;
                case "DivXY":
                    button.Background = "#9cffe7".GetBrush();
                    break;
                case "Pow":
                    button.Background = "#9cffe7".GetBrush();
                    break;
                case "Abs":
                    button.Background = "#9cffe7".GetBrush();
                    break;

                case "Div":
                    button.Background = "#dfdfdf".GetBrush();
                    break;
                case "Mul":
                    button.Background = "#dfdfdf".GetBrush();
                    break;
                case "Plus":
                    button.Background = "#dfdfdf".GetBrush();
                    break;
                case "Minus":
                    button.Background = "#dfdfdf".GetBrush();
                    break;
                case "Mod":
                    button.Background = "#dfdfdf".GetBrush();
                    break;
                case "Dot":
                    button.Background = "#dfdfdf".GetBrush();
                    break;

                default:
                    button.Background = "#30ffffff".GetBrush();
                    break;
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Border;
            var name = button.Name.Replace("uiButton", "");
            switch (name)
            {
                case "Br1":
                    button.Background = "#ff956e".GetBrush();
                    break;
                case "Br2":
                    button.Background = "#ff956e".GetBrush();
                    break;
                case "MinusX":
                    button.Background = "#ff956e".GetBrush();
                    break;

                case "NSqrt":
                    button.Background = "#49e8c2".GetBrush();
                    break;
                case "Sqrt":
                    button.Background = "#49e8c2".GetBrush();
                    break;
                case "DivXY":
                    button.Background = "#49e8c2".GetBrush();
                    break;
                case "Pow":
                    button.Background = "#49e8c2".GetBrush();
                    break;
                case "Abs":
                    button.Background = "#49e8c2".GetBrush();
                    break;

                case "Div":
                    button.Background = "#ffffff".GetBrush();
                    break;
                case "Mul":
                    button.Background = "#ffffff".GetBrush();
                    break;
                case "Plus":
                    button.Background = "#ffffff".GetBrush();
                    break;
                case "Minus":
                    button.Background = "#ffffff".GetBrush();
                    break;
                case "Mod":
                    button.Background = "#ffffff".GetBrush();
                    break;
                case "Dot":
                    button.Background = "#ffffff".GetBrush();
                    break;

                default:
                    button.Background = "#00000000".GetBrush();
                    break;
            }
        }

        private void Border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            var name = button.Name.Replace("uiButton", "");
            switch (name)
            {
                case "Br1":
                    OnClick?.Invoke(this, "(");
                    break;
                case "MinusX":
                    OnClick?.Invoke(this, "minX");
                    break;

                case "NSqrt":
                    OnClick?.Invoke(this, "nsqrt");
                    break;
                case "Sqrt":
                    OnClick?.Invoke(this, "sqrt");
                    break;
                case "DivXY":
                    OnClick?.Invoke(this, "divide");
                    break;
                case "Pow":
                    OnClick?.Invoke(this, "pow");
                    break;
                case "Abs":
                    OnClick?.Invoke(this, "abs");
                    break;

                case "Div":
                    OnClick?.Invoke(this, "/");
                    break;
                case "Mul":
                    OnClick?.Invoke(this, "*");
                    break;
                case "Plus":
                    OnClick?.Invoke(this, "+");
                    break;
                case "Minus":
                    OnClick?.Invoke(this, "-");
                    break;
                case "Mod":
                    OnClick?.Invoke(this, "%");
                    break;
                case "Dot":
                    OnClick?.Invoke(this, ",");
                    break;

                default:
                    OnClick?.Invoke(this, name);
                    break;
            }
        }

        private void Border_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            var name = button.Name.Replace("uiButton", "");
            switch (name)
            {
                case "Div":
                    OnClick?.Invoke(this, "f/");
                    break;
                case "Mul":
                    OnClick?.Invoke(this, "f*");
                    break;
                case "Plus":
                    OnClick?.Invoke(this, "f+");
                    break;
                case "Minus":
                    OnClick?.Invoke(this, "f-");
                    break;
                case "Mod":
                    OnClick?.Invoke(this, "f%");
                    break;
            }
        }
    }
}
