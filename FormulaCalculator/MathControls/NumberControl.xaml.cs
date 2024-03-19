using FormulaCalculator.Math.Elements;
using FormulaCalculator.Math.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;

namespace FormulaCalculator.MathControls
{
    /// <summary>
    /// Логика взаимодействия для NumberControl.xaml
    /// </summary>
    public partial class NumberControl : UserControl
    {
        public event EventHandler OnDelete;                                         //событие, которое создано для попытки удалить элемент
        public event EventHandler OnEdit;                                           //событие, которое создано для уведомления о том, что текст  в текстбоксе изменен
        internal NumberElement Element { get; private set; }                        //элемент
        public double? Value { get => Element.Value; set => Element.Value = value; }    //значение элемента

        public bool IsConstant { get; set; }                                        //если текущий элемент является константой
        internal Constants Const { get; set; }                                      //устанавливается, какой именно он константой является
        public NumberControl()
        {
            InitializeComponent();
            Element = new NumberElement(this);                                      //создание элемента, и передача Instance текущего класса элементу
        }

        public void SetFocus()                                                      //Устанавливает фокус клавиатуры (курсор) в текущем текстбоксе
        {
            this.Invoke(() =>
            {
                uiInput.Focus();
            });
        }

        public bool Focused { get => uiInput.IsFocused; }                           //поставлен ли фокус клавиатуры (курсор) у элемента

        private void Border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) //при нажатии на графический элемент, фокусируется на текстбоксе
        {
            SetFocus();
        }

        private void uiInput_PreviewKeyUp(object sender, KeyEventArgs e)    //проверка, не хочет ли пользователь удалить элемент
        {
            if(e.Key == Key.Delete)
            {
                if(uiInput.Text.Length < 1)
                    OnDelete?.Invoke(this, new EventArgs());
            }
        }

        public void Insert(string str)                                      //вставляет значения в элемент (графический текст)
        {
            if (!IsConstant)
            {
                if (str == "-" && uiInput.Text.Length > 0 && (uiInput.Text[0] == '-' || char.IsDigit(uiInput.Text[0]) || uiInput.Text[0] == '.'))
                    return;

                uiInput.Text += str;
                uiInput_TextChanged(uiInput, null);
                uiInput.CaretIndex = uiInput.Text.Length;
            }
        }

        internal void SetConstant(Constants constant)                       //устанавливает этот элемент как константу
        {
            Const = constant;
            IsConstant = true;
            switch (Const)
            {
                case Constants.Pi:
                    uiInput.Text = "π";
                    break;
                case Constants.E:
                    uiInput.Text = "e";
                    break;
                case Constants.Phi:
                    uiInput.Text = "φ";
                    break;
            }
            uiInput_TextChanged(uiInput, null);
            uiInput.CaretIndex = uiInput.Text.Length;
        }

        private void uiInput_PreviewTextInput(object sender, TextCompositionEventArgs e)    //проверка на ввод только цифр, минуса и точки
        {
            if (!IsConstant)
            {
                Regex regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else
            {
                Regex regex = new Regex("[^πeφ]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        public void Erase()                                                 //аналог Backscape (стереть) на физической клавиатуре
        {
            if(uiInput.Text.Length < 1 && !IsConstant)
                OnDelete?.Invoke(this, new EventArgs());
            else if (uiInput.Text.Length < 1 && IsConstant) //если стираем константу, то элемент снова становится обычным числом
            {
                IsConstant = false;
                Value = null;
            }
            else
            {
                int index = uiInput.CaretIndex - 1;
                if (index < 0)
                    index = uiInput.Text.Length - 1;
                uiInput.Text = uiInput.Text.Remove(index, 1);
                uiInput.CaretIndex = index;
                uiInput_TextChanged(uiInput, null);
            }
        }

        private void uiInput_TextChanged(object sender, TextChangedEventArgs e) //событие, возникающее при изменении текста
        {
            try
            {
                if (uiInput.Text.Length > 0 && !IsConstant)
                    Value = double.Parse(uiInput.Text.Replace(" ", ""), CultureInfo.InvariantCulture);
                else if (uiInput.Text.Length > 0 && IsConstant)
                {
                    switch (Const)
                    {
                        case Constants.Pi:
                            Value = 3.141592653589793238;
                            break;
                        case Constants.E:
                            Value = 2.718281828459045235;
                            break;
                        case Constants.Phi:
                            Value = 1.618033988749894848;
                            break;
                    }
                }
                else if (uiInput.Text.Length < 1 && IsConstant)
                {
                    IsConstant = false;
                    Value = null;
                }
                else
                    Value = null;
                OnEdit?.Invoke(this, new EventArgs());
            }
            catch { }
        }
    }
}
