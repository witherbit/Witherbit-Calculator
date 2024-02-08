using FormulaCalculator.Math.Utils;
using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class AbsElement : IMathElement
    {
        //Элемент с одним стеком
        public object Instance { get; } //Экземпляр графического элемента, к которому привязан текущий элемент
        public MathNumerable Elements { get => (Instance as AbsControl).ThisStack.Handler.Elements; }   //Возвращает элементы из стека элемента

        public AbsElement(object instance)
        {
            Instance = instance;
        }

        public string Calculate()   //считает значения из стека элемента, а затем считает модуль этих значений и возвращает результат
        {
            var numerator = Elements.CreateNumerator();
            var evaluationString = "";
            while (numerator.HasNext())
            {
                evaluationString += numerator.CalculateNext();
            }
            return System.Math.Abs(evaluationString.Evaluate()).ToString();
        }

        public void RemoveStack()   //удаляет стек этого элемента (OLD | не использовать)
        {
            var control = Instance as AbsControl;
            Stack.RemoveStack(control.ThisStack);
        }
    }
}
