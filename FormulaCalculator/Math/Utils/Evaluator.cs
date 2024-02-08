using org.matheval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteByte.Tools;

namespace FormulaCalculator.Math.Utils
{
    internal static class Evaluator
    {
        public static double Evaluate(this string expression) //считает выражение в виде строки
        {
            //return expression.Replace(",", ".").Calculate();
            Expression eval = new Expression(expression.Replace(",", "."));
            return eval.Eval<double>();
        }
    }
}
