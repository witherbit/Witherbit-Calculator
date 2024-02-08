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
    /// Логика взаимодействия для DivideExtraControl.xaml
    /// </summary>
    public partial class DivideExtraControl : UserControl
    {
        //описание графического элемента с двумя стеками
        internal Stack ThisStack { get; private set; }      //стек числителя
        internal Stack ThisLowStack { get; private set; }   //стек знаменателя
        internal DivideElement Element { get; private set; }//элемент
        public DivideExtraControl()
        {
            InitializeComponent();
            Element = new DivideElement(this);              //создаем элемент, и передаем ему Instance текущего графического элемента)
            ThisStack = Stack.AddStack(new Stack(uiInput, this));   //создаем стек числителя
            ThisLowStack = Stack.AddStack(new Stack(uiLowInput, this)); //создаем стек знаменателя
            ThisStack.Reset();  //устанавливаем элементы стеков в первоначальное положение
            ThisLowStack.Reset();
        }
    }
}
