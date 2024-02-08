using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Operators
{
    internal class MinusOperator : IMathElement
    {
        public MathNumerable Elements { get; set; }

        public object Instance { get; }
        public MinusOperator(object instance)
        {
            Instance = instance;
        }
        public string Calculate()
        {
            return "-";
        }
        public void RemoveStack()
        {

        }
    }
}
