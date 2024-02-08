using FormulaCalculator.Math.Utils;
using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class PowSqrtElement : IMathElement
    {
        public object Instance { get; }
        public MathNumerable Elements { get => (Instance as NsqrtControl).ThisStack.Handler.Elements; }
        public MathNumerable PowElements { get => (Instance as NsqrtControl).ThisPowStack.Handler.Elements; }

        public PowSqrtElement(object instance)
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
            return System.Math.Pow(evaluationString.Evaluate(), 1 / powEvaluationString.Evaluate()).ToString();
        }

        public void RemoveStack()
        {
            var control = Instance as NsqrtControl;
            Stack.RemoveStack(control.ThisStack);
            Stack.RemoveStack(control.ThisPowStack);
        }
    }
}
