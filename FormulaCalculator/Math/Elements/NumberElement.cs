using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class NumberElement : IMathElement
    {
        public object Instance { get; }             //графический элемент, содержащий текущий элемент
        public MathNumerable Elements { get; set; } //элементы текущего элемента, не актуально для NumberElement

        public double? Value { get; set; }          //значение текущего элемента, может быть = null

        public NumberElement(double value, object instance)
        {
            Value = value;
            Instance = instance;
        }
        public NumberElement(object instance)
        {
            Instance = instance;
        }

        public string Calculate()                   //возвращает Value
        {
            if (Value != null)
                return Value.ToString();
            else
                return "";
        }
        public void RemoveStack()
        {
            
        }
    }
}
