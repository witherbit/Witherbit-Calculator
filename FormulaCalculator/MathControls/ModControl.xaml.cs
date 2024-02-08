using FormulaCalculator.Math.Operators;
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
    /// Логика взаимодействия для ModControl.xaml
    /// </summary>
    public partial class ModControl : UserControl
    {
        internal ModOperator Operator { get; private set; }
        public ModControl()
        {
            InitializeComponent();
            Operator = new ModOperator(this);
        }
    }
}
