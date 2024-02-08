using WhiteByte.Utils;
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
using MaterialDesignThemes.Wpf;
using WhiteByte.Tools;
using FormulaCalculator.Math.Elements;
using FormulaCalculator.Math.Operators;
using FormulaCalculator.Math;
using FormulaCalculator.Math.Utils;
using FormulaCalculator.Math.Enums;
using FormulaCalculator.MathControls;
using System.Xml.Linq;

namespace FormulaCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static MainWindow Instance { get; private set; }       //для обращения к существующему экземпляру класса MainWindow
        internal Stack ThisStack { get; private set; }                  //главный стек для подсчета

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            StateChanged += StateChange;                                //для анимации состояния окна при сворацивании
            Stack.Evaluate += OnEvaluate;                               //подписываемся на ивент, который обновляет состояние ответа
            ThisStack = Stack.AddStack(new Stack(uiInputStack, this));  //добавляет главный стек елементов
            ThisStack.SetParent();                                      //делаем текущий стек родительским
        }

        private void OnEvaluate(object sender, string e)                //Обновление текста ответа, этот метод подписан на событие Stack.Evaluate
        {
            uiTextSolution.Text = e;
        }

        // Верхний бар (открыть, закрыть, перетаскивать окно)
        #region TopBar                                                  

        private void Window_Loaded(object sender, RoutedEventArgs e)    //При загрузке окна происходит размытие
        {
            this.EnableBlur();
        }

        private void StateChange(object sender, EventArgs e)            //анимация при сворачивании окна, постепенно меняет прозрачность
        {
            try
            {
                this.OpacityAnimation(0, 1, 0.1);
            }
            catch (Exception ex)
            {
            }
        }

        private void UI_MouseEnter(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            if (grid == uiGridButtonExit)
            {
                ellipseClose.Fill = "#fe90ab".GetBrush();
            }
            else if (grid == uiGridButtonMinimize)
            {
                ellipseMinimize.Fill = "#76ffde".GetBrush();
            }
        }

        private void UI_MouseLeave(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            if (grid == uiGridButtonExit)
            {
                ellipseClose.Fill = "#ff5c83".GetBrush();
            }
            else if (grid == uiGridButtonMinimize)
            {
                ellipseMinimize.Fill = "#49e8c2".GetBrush();
            }
        }
        private async void UI_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            if (grid == uiGridButtonExit)
            {
                Close();
            }
            else if (grid == uiGridButtonMinimize)
            {
                this.OpacityAnimation(1, 0, 0.2);
                await Task.Run(() =>
                {
                    Task.Delay(200).Wait();
                    this.Invoke(new Action(() =>
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Minimized;
                    }));
                });
            }
        }

        private void uiGridTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion                                                      

        private void StackPanel_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) //переключение клавиатуры редактора формул с цифровой на функциональную и обратно
        {
            if(ui123Check.Visibility == Visibility.Collapsed)
            {
                ui123Check.Visibility = Visibility.Visible;
                uiSinCheck.Visibility = Visibility.Collapsed;
                uiKeyboardCalc.Visibility = Visibility.Visible;
                uiKeyboardFunk.Visibility = Visibility.Collapsed;
            }
            else
            {
                ui123Check.Visibility = Visibility.Collapsed;
                uiSinCheck.Visibility = Visibility.Visible;
                uiKeyboardCalc.Visibility = Visibility.Collapsed;
                uiKeyboardFunk.Visibility = Visibility.Visible;
            }
        }

        private void CalcKeyboard_OnClick(object sender, string e)  //событие, происходящее при нажатии на кнопку экранной клавиатуры
        {
            var stack = Stack.GetFocusedStack();                    //ищем стек, в котором клавиатура сфокусирована на определенном элементе
            if (stack != null)                                      //если такой есть, то
                stack.Input(e);                                     //отправляем значение в стек
        }

        // Кнопка CE как в калькуляторах, возвращает стеки в исзодное состояние
        #region CE Button
        private void uiButtonClear_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Border;
            button.Background = "#ffb397".GetBrush();
        }

        private void uiButtonClear_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Border;
            button.Background = "#ff956e".GetBrush();
        }

        private void uiButtonClear_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Stack.General.Clear();
            ThisStack = Stack.AddStack(new Stack(uiInputStack, this));
            ThisStack.SetParent();
        }
        #endregion

        // Кнопка, аналог которой клавиша Backscape (стереть) на физической клавиатуре
        #region Erase Button
        private void uiButtonBackscape_MouseEnter(object sender, MouseEventArgs e)
        {
            uiButtonBackscape.Background = "#61f6d2".GetBrush();
        }

        private void uiButtonBackscape_MouseLeave(object sender, MouseEventArgs e)
        {
            uiButtonBackscape.Background = "#49e8c2".GetBrush();
        }

        private void uiButtonBackscape_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var stack = Stack.GetFocusedStack();
            if(stack != null)
                stack.GetFocusedElement().Erase();
        }
        #endregion
    }
}
