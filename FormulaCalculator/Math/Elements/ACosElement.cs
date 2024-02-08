using FormulaCalculator.Math.Utils;
using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class ACosElement : IMathElement
    {
        public object Instance { get; }
        public MathNumerable Elements { get => (Instance as ACosControl).ThisStack.Handler.Elements; }

        public ACosElement(object instance)
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
            var result = System.Math.Acos(evaluationString.Evaluate());
            if (result.ToString().Contains("E"))
                result = System.Math.Round(result, 2);
            return result.ToString();
        }
        public void RemoveStack()
        {
            var control = Instance as ACosControl;
            Stack.RemoveStack(control.ThisStack);
        }
    }
}
