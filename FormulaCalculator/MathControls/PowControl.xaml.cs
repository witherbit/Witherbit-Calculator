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
    /// Логика взаимодействия для PowControl.xaml
    /// </summary>
    public partial class PowControl : UserControl
    {
        internal Stack ThisStack {  get; private set; }
        internal Stack ThisPowStack { get; private set; }
        internal PowElement Element { get; private set; }
        public PowControl()
        {
            InitializeComponent();
            Element = new PowElement(this);
            ThisStack = Stack.AddStack(new Stack(uiInput, this));
            ThisPowStack = Stack.AddStack(new Stack(uiPowInput, this));
            ThisPowStack.IsPow = true;
            ThisPowStack.MinWidth = 10;
            ThisPowStack.MinHeight = 10;
            ThisStack.Reset();
            ThisPowStack.Reset();
        }
    }
}
