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
using WhiteByte.Utils;

namespace FormulaCalculator.MathControls
{
    /// <summary>
    /// Логика взаимодействия для BracketsControl.xaml
    /// </summary>
    public partial class BracketsControl : UserControl
    {
        internal Stack ThisStack { get; private set; }
        internal BracketsElement Element { get; private set; }
        public BracketsControl(Stack parent)
        {
            InitializeComponent();
            var brushHigh = 0x1000000.GenerateRandomColor().GetBrush();
            uiHigh1.Foreground = brushHigh;
            uiHigh2.Foreground = brushHigh;
            Element = new BracketsElement(this);
            ThisStack = Stack.AddStack(new Stack(uiInput, this) { Parent = parent, InnerIndex = parent.LastFocusedIndex });
            ThisStack.Reset();
        }
    }
}
