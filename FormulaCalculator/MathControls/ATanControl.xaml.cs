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
    /// Логика взаимодействия для ATanControl.xaml
    /// </summary>
    public partial class ATanControl : UserControl
    {
        internal Stack ThisStack { get; private set; }
        internal ATanElement Element { get; private set; }
        public ATanControl()
        {
            InitializeComponent();
            Element = new ATanElement(this);
            ThisStack = Stack.AddStack(new Stack(uiInput, this));
            ThisStack.Reset();
        }
    }
}
