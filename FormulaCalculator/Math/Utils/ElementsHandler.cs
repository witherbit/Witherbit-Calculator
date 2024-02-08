using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Utils
{
    internal class ElementsHandler
    {
        public MathNumerable Elements { get; }  //IMathElement элементы

        public ElementsHandler()
        {
            Elements = new MathNumerable();
        }
        public double Calculate()               //Считает значение всех элементов по принципу Матрешки с помощью итератора, возвращает значение стека
        {
            var iterator = Elements.CreateNumerator();
            var str = "";
            while (iterator.HasNext())
            {
                str += iterator.CalculateNext();
            }
            return str.Evaluate();
        }
    }
}
