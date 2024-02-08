using FormulaCalculator.Math.Utils;
using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class FloorElement : IMathElement
    {
        public object Instance { get; }
        public MathNumerable Elements { get => (Instance as FloorControl).ThisStack.Handler.Elements; }

        public FloorElement(object instance)
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
            return System.Math.Floor(evaluationString.Evaluate()).ToString();
        }

        public void RemoveStack()
        {
            var control = Instance as FloorControl;
            Stack.RemoveStack(control.ThisStack);
        }
    }
}
