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
    /// Логика взаимодействия для ACosControl.xaml
    /// </summary>
    public partial class ACosControl : UserControl
    {
        internal Stack ThisStack { get; private set; }
        internal ACosElement Element { get; private set; }
        public ACosControl()
        {
            InitializeComponent();
            Element = new ACosElement(this);
            ThisStack = Stack.AddStack(new Stack(uiInput, this));
            ThisStack.Reset();
        }
    }
}
