using FormulaCalculator.Math.Utils;
using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class LogElement : IMathElement
    {
        public object Instance { get; }
        public MathNumerable Elements { get => (Instance as LogControl).ThisStack.Handler.Elements; }

        public LogElement(object instance)
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
            var result = System.Math.Log10(evaluationString.Evaluate());
            if (result.ToString().Contains("E"))
                result = System.Math.Round(result, 2);
            return result.ToString();
        }

        public void RemoveStack()
        {
            var control = Instance as LogControl;
            Stack.RemoveStack(control.ThisStack);
        }
    }
}
