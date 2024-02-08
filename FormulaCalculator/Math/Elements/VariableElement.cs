using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class VariableElement : IMathElement
    {
        public object Instance { get; }
        public MathNumerable Elements { get; set; }

        public double Value { get; set; }

        public char Variable {  get; set; }

        public VariableElement(object instance)
        {
            Elements = new MathNumerable();
            Instance = instance;
        }

        public string Calculate()
        {
            return Value.ToString();
        }

        public void RemoveStack()
        {
        }
    }
}
