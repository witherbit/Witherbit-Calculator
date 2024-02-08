using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math
{
    internal class MathIterator : IMathIterator
    {
        IMathNumerable aggregate;
        int index = 0;
        public MathIterator(IMathNumerable a)
        {
            aggregate = a;
        }

        public string CalculateNext()
        {
            return aggregate[index++].Calculate();
        }

        public bool HasNext()
        {
            return index < aggregate.Count;
        }

        public IMathElement Next()
        {
            return aggregate[index++];
        }
    }
}
