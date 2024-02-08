using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math
{
    internal interface IMathElement
    {
        MathNumerable Elements { get; } //Список элементов внутри текущего элемента
        object Instance { get; }        //Экземпляр графического элемента, к которому привязан текущий элемент

        string Calculate();             //Посчитать с помощью итератора
        void RemoveStack();             //Удалить стек этого элемента (OLD)
    }
}
