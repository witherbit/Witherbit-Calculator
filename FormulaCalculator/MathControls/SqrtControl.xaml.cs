using FormulaCalculator.Math.Elements;
using FormulaCalculator.Math;
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
using FormulaCalculator.Math.Utils;

namespace FormulaCalculator.MathControls
{
    /// <summary>
    /// Логика взаимодействия для SqrtControl.xaml
    /// </summary>
    public partial class SqrtControl : UserControl
    {
        internal Stack ThisStack { get; private set; }
        internal SqrtElement Element { get; private set; }
        public SqrtControl(Stack parent)
        {
            InitializeComponent();
            Element = new SqrtElement(this);
            ThisStack = Stack.AddStack(new Stack(uiInput, this) { Parent = parent, InnerIndex = parent.LastFocusedIndex });
            ThisStack.Reset();
        }
    }
}
