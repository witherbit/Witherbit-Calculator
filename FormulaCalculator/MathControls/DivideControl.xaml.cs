using FormulaCalculator.Math.Operators;
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
    /// Логика взаимодействия для DivideControl.xaml
    /// </summary>
    public partial class DivideControl : UserControl
    {
        internal DivideOperator Operator { get; private set; }
        public DivideControl()
        {
            InitializeComponent();
            Operator = new DivideOperator(this);
        }
    }
}
