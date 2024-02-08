using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Operators
{
    internal class MultiplyOperator : IMathElement
    {
        public MathNumerable Elements { get; set; }
        public object Instance { get; }
        public MultiplyOperator(object instance)
        {
            Instance = instance;
        }
        public string Calculate()
        {
            return "*";
        }

        public void RemoveStack()
        {

        }
    }
}
