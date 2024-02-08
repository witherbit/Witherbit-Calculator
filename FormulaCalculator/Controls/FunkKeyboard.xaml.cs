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
    /// Логика взаимодействия для FunkKeyboard.xaml
    /// </summary>
    public partial class FunkKeyboard : UserControl
    {
        public event EventHandler<string> OnClick;
        public FunkKeyboard()
        {
            InitializeComponent();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Border;
            var name = button.Name.Replace("uiButton", "");
            switch (name)
            {
                case "Sin":
                    button.Background = "#ffb397".GetBrush();
                    break;
                case "Cos":
                    button.Background = "#ffb397".GetBrush();
                    break;
                case "Tan":
                    button.Background = "#ffb397".GetBrush();
                    break;
                case "ASin":
                    button.Background = "#ffb397".GetBrush();
                    break;
                case "ACos":
                    button.Background = "#ffb397".GetBrush();
                    break;
                case "ATan":
                    button.Background = "#ffb397".GetBrush();
                    break;

                case "Log":
                    button.Background = "#9cffe7".GetBrush();
                    break;
                case "Ln":
                    button.Background = "#9cffe7".GetBrush();
                    break;

                case "Deg":
                    button.Background = "#30ffffff".GetBrush();
                    break;
                case "Pi":
                    button.Background = "#30ffffff".GetBrush();
                    break;
                case "E":
                    button.Background = "#30ffffff".GetBrush();
                    break;
                case "Phi":
                    button.Background = "#30ffffff".GetBrush();
                    break;

                default:
                    button.Background = "#dfdfdf".GetBrush();
                    break;
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Border;
            var name = button.Name.Replace("uiButton", "");
            switch (name)
            {
                case "Sin":
                    button.Background = "#ff956e".GetBrush();
                    break;
                case "Cos":
                    button.Background = "#ff956e".GetBrush();
                    break;
                case "Tan":
                    button.Background = "#ff956e".GetBrush();
                    break;
                case "ASin":
                    button.Background = "#ff956e".GetBrush();
                    break;
                case "ACos":
                    button.Background = "#ff956e".GetBrush();
                    break;
                case "ATan":
                    button.Background = "#ff956e".GetBrush();
                    break;

                case "Log":
                    button.Background = "#49e8c2".GetBrush();
                    break;
                case "Ln":
                    button.Background = "#49e8c2".GetBrush();
                    break;

                case "Deg":
                    button.Background = "#00000000".GetBrush();
                    break;
                case "Pi":
                    button.Background = "#00000000".GetBrush();
                    break;
                case "E":
                    button.Background = "#00000000".GetBrush();
                    break;
                case "Phi":
                    button.Background = "#00000000".GetBrush();
                    break;
                    break;

                default:
                    button.Background = "#ffffff".GetBrush();
                    break;
            }
        }

        private void Border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            var name = button.Name.Replace("uiButton", "");
            switch (name)
            {
                case "Sin":
                    OnClick?.Invoke(this, "sin");
                    break;
                case "Cos":
                    OnClick?.Invoke(this, "cos");
                    break;
                case "Tan":
                    OnClick?.Invoke(this, "tan");
                    break;
                case "ASin":
                    OnClick?.Invoke(this, "asin");
                    break;
                case "ACos":
                    OnClick?.Invoke(this, "acos");
                    break;
                case "ATan":
                    OnClick?.Invoke(this, "atan");
                    break;

                case "Log":
                    OnClick?.Invoke(this, "log");
                    break;
                case "Ln":
                    OnClick?.Invoke(this, "ln");
                    break;

                case "Deg":
                    OnClick?.Invoke(this, "deg");
                    break;
                case "Pi":
                    OnClick?.Invoke(this, "PI");
                    break;
                case "E":
                    OnClick?.Invoke(this, "e");
                    break;
                case "Phi":
                    OnClick?.Invoke(this, "Phi");
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
                case "Round":
                    OnClick?.Invoke(this, "round");
                    break;
                case "Ceil":
                    OnClick?.Invoke(this, "ceil");
                    break;
                case "Floor":
                    OnClick?.Invoke(this, "floor");
                    break;
            }
        }
    }
}
