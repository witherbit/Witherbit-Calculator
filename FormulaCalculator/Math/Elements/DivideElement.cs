using FormulaCalculator.Math.Utils;
using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math.Elements
{
    internal class DivideElement : IMathElement
    {
        //Элемент с двумя стеками
        public object Instance { get; } //Экземпляр графического элемента, к которому привязан текущий элемент
        public MathNumerable Elements { get => (Instance as DivideExtraControl).ThisStack.Handler.Elements; }               //Возвращает элементы числителя из первого стека элемента
        public MathNumerable ElementsSecondary { get => (Instance as DivideExtraControl).ThisLowStack.Handler.Elements; }   //Возвращает элементы знаменателя из второго стека элемента

        public DivideElement(object instance)
        {
            Instance = instance;
        }

        public string Calculate()                                                                       //считает выражение
        {
            var numerator = Elements.CreateNumerator();                                                 //итератор для первого стека
            var evaluationString = "";                                                                  //строка выражения для первого стека
            while (numerator.HasNext())
                evaluationString += numerator.CalculateNext();

            var numeratorSecondary = ElementsSecondary.CreateNumerator();                               //итератор для второго стека
            var evaluationStringSecondary = "";                                                         //строка выражения для второго стека
            while (numeratorSecondary.HasNext())
                evaluationStringSecondary += numeratorSecondary.CalculateNext();

            return (evaluationString.Evaluate() / evaluationStringSecondary.Evaluate()).ToString();     //возвращает результаты деления значения первого стека (числитель) на значение второго стека (значенатель) 
        }

        public void RemoveStack()   //Удаляет стеки этого элемента (OLD | не использовать)
        {
            var control = Instance as DivideExtraControl;
            Stack.RemoveStack(control.ThisStack);
            Stack.RemoveStack(control.ThisLowStack);
        }
    }
}
