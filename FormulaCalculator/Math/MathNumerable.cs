using FormulaCalculator.Math.Enums;
using FormulaCalculator.Math.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace FormulaCalculator.Math
{
    internal class MathNumerable : IMathNumerable
    {
        private List<IMathElement> _elements;
        public MathNumerable()
        {
            _elements = new List<IMathElement>();
        }
        public IMathElement this[int index]
        {
            get { return _elements[index]; }
        }

        public int Count
        {
            get { return _elements.Count; }
        }

        public void Add(IMathElement element)
        {
            _elements.Add(element);
        }
        public void Remove(IMathElement element)
        {
            _elements.Remove(element);
        }
        public void RemoveAt(int index)
        {
            _elements.RemoveAt(index);
        }
        public void Clear()
        {
            _elements.Clear();
        }

        public IMathIterator CreateNumerator()
        {
            return new MathIterator(this);
        }

        public IMathElement Add(Operator @operator, object instance)
        {
            switch (@operator)
            {
                case Operator.Plus:
                    var plus = new PlusOperator(instance);
                    Add(plus);
                    return plus;
                case Operator.Minus:
                    var minus = new MinusOperator(instance);
                    Add(minus);
                    return minus;
                case Operator.Divide:
                    var divide = new DivideOperator(instance);
                    Add(divide);
                    return divide;
                case Operator.Multiply:
                    var mO = new MultiplyOperator(instance);
                    Add(mO);
                    return mO;
                case Operator.Mod:
                    var mod = new ModOperator(instance);
                    Add(mod);
                    return mod;
                default:
                    return null;
            }
        }

        public void Insert(int index, IMathElement element)
        {
            _elements.Insert(index, element);
        }
    }
}
