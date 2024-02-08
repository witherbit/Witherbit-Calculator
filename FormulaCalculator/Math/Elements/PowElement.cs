using FormulaCalculator.Math.Utils;
using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class PowElement : IMathElement
    {
        public object Instance { get; }
        public MathNumerable Elements { get => (Instance as PowControl).ThisStack.Handler.Elements; }
        public MathNumerable PowElements { get => (Instance as PowControl).ThisPowStack.Handler.Elements; }

        public PowElement(object instance)
        {
            Instance = instance;
        }

        public string Calculate()
        {
            var numerator = Elements.CreateNumerator();
            var powNumerator = PowElements.CreateNumerator();
            var evaluationString = "";
            var powEvaluationString = "";
            while (numerator.HasNext())
            {
                evaluationString += numerator.CalculateNext();
            }
            while (powNumerator.HasNext())
            {
                powEvaluationString += powNumerator.CalculateNext();
            }
            return System.Math.Pow(evaluationString.Evaluate(), powEvaluationString.Evaluate()).ToString();
        }

        public void RemoveStack()
        {
            var control = Instance as PowControl;
            Stack.RemoveStack(control.ThisStack);
            Stack.RemoveStack(control.ThisPowStack);
        }
    }
}
