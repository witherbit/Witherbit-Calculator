using FormulaCalculator.Math.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCalculator.Math
{
    internal interface IMathNumerable
    {
        IMathIterator CreateNumerator();                        //Создает итератор
        int Count { get; }                                      //Возвращает кол-во элементов
        IMathElement this[int index] { get; }                   
        void Add(IMathElement element);                         //Добавляет элемент в список
        IMathElement Add(Operator @operator, object instance);  //Добавляет математический элемент оператора (быстрый доступ) (OLD)
        void Remove(IMathElement element);                      //Удаляет элемент из списка
        void RemoveAt(int index);                               //Удаляет элемент из списка по индексу
        void Clear();                                           //Очищает список элементов
        void Insert(int index, IMathElement element);           //Вставляет элемент в определенное место
    }
}
