﻿using FormulaCalculator.Math.Utils;
using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class RoundElement : IMathElement
    {
        public object Instance { get; }
        public MathNumerable Elements { get => (Instance as RoundControl).ThisStack.Handler.Elements; }

        public RoundElement(object instance)
        {
            Instance = instance;
        }

        public string Calculate()
        {
            var numerator = Elements.CreateNumerator();
            var evaluationString = "";
            while (numerator.HasNext())
            {
                evaluationString += numerator.CalculateNext();
            }
            return System.Math.Round(evaluationString.Evaluate()).ToString();
        }

        public void RemoveStack()
        {
            var control = Instance as RoundControl;
            Stack.RemoveStack(control.ThisStack);
        }
    }
}
