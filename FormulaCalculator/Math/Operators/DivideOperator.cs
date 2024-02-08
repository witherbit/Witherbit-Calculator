using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Operators
{
    internal class DivideOperator : IMathElement
    {
        public MathNumerable Elements { get; set; } //Элементы текущего элемента не актуальны для операторов, реализующих IMathElement
        public object Instance { get; }             //Графический элемент, которому принадлежит текущий элемент
        public DivideOperator(object instance)
        {
            Instance = instance;
        }

        public string Calculate()                   //Возвращает оператор, актуально для всех операторов, реализующих IMathElement
        {
            return "/";
        }

        public void RemoveStack()
        {
            
        }
    }
}
