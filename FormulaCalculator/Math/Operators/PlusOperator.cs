using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Operators
{
    internal class PlusOperator : IMathElement
    {
        public MathNumerable Elements { get; set; }
        public object Instance { get; }
        public PlusOperator(object instance)
        {
            Instance = instance;
        }
        public string Calculate()
        {
            return "+";
        }

        public void RemoveStack()
        {

        }
    }
}
