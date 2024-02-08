using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math
{
    internal interface IMathIterator
    {
        bool HasNext();             //если есть след. элемент, то true, если нету, то false
        IMathElement Next();        //возвращает след.элемент
        string CalculateNext();     //выполняет операцию подсчета выражения стека элемента и возвращает значение выражения
    }
}
