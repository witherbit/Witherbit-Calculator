using FormulaCalculator.Math;
using FormulaCalculator.Math.Elements;
using FormulaCalculator.Math.Utils;
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

namespace FormulaCalculator.MathControls
{
    /// <summary>
    /// Логика взаимодействия для AbsControl.xaml
    /// </summary>
    public partial class AbsControl : UserControl
    {
        //описание графического элемента с одним стеком
        internal Stack ThisStack { get; private set; }  //стек элемента
        internal AbsElement Element { get; private set; } //элемент
        public AbsControl()
        {
            InitializeComponent();
            Element = new AbsElement(this); //создаем жлемент и передаем ему Instance текущего графического элемента
            ThisStack = Stack.AddStack(new Stack(uiInput, this));   //создаем стек
            ThisStack.Reset();              //устанавливаем элементы стека в первоначальное положение
        }
    }
}
