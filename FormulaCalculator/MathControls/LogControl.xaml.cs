﻿using FormulaCalculator.Math.Elements;
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
    /// Логика взаимодействия для LogControl.xaml
    /// </summary>
    public partial class LogControl : UserControl
    {
        internal Stack ThisStack { get; private set; }
        internal LogElement Element { get; private set; }
        public LogControl(Stack parent)
        {
            InitializeComponent();
            Element = new LogElement(this);
            ThisStack = Stack.AddStack(new Stack(uiInput, this) { Parent = parent, InnerIndex = parent.LastFocusedIndex });
            ThisStack.Reset();
        }
    }
}
