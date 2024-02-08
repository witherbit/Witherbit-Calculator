using FormulaCalculator.MathControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WhiteByte.Utils;

namespace FormulaCalculator.Math.Utils
{
    internal class Stack
    {
        internal static List<Stack> General { get; private set; } = new List<Stack>();      //общий список элементов
        internal static Stack Parent { get; private set; }                                  //родительский стек
        internal static event EventHandler<string> Evaluate;                                //событие изменения значений
        internal static event EventHandler<string> Exception;                               //событие для отладки Evaluator'а
        internal object Instance { get; private set; }                                      //графический элемент, которому принадлежит текущий стек
        internal StackPanel Panel { get; set; }                                             //панель, в которой отображаются элементы
        internal UIElementCollection Children { get => Panel.Children; }                    //элементы Panel
        internal ElementsHandler Handler { get; private set; }                              //обработчик и контейнер элементов текущего стека
        internal double MinHeight { get; set; } = 30;                                       //минимальная высота - нужна для отображения в виде степени или простого числа НЕ МЕНЯТЬ
        internal double MinWidth { get; set; } = 30;                                        //минимальная ширина - нужна для отображения в виде степени или простого числа НЕ МЕНЯТЬ
        internal bool IsPow {  get; set; }                                                  //стек является стеком для степенных значений
        internal bool StackIsFocused { get => GetFocusedIndex() > -1; }                     //возвращает true, если фокус клавиатуры на текущем стеке

        private void OnDeleteElement(object sender, EventArgs e)                            //метод, подписанный на событие, происходящее, когда пользователь хочет удалить элемент
        {
            int focused = GetFocusedIndex();                                                //получаем элемент, который хочет удалить/отредактировать пользователь
            if (focused >= 0 && Handler.Elements.Count > 1)                                 
            {
                if(focused == 0 && Handler.Elements.Count > 3)                              //удаляет первый элемент, если в стеке есть более трех элементов
                {
                    Handler.Elements.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    Children.RemoveAt(focused);
                    Children.RemoveAt(focused);
                    (Children[focused] as NumberControl).SetFocus();
                    OnEditElement(sender, null);
                }
                else if (focused != 0)                                                      //удаляет элемент, который не является первым
                {
                    Handler.Elements.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused - 1);
                    Children.RemoveAt(focused);
                    Children.RemoveAt(focused - 1);
                    try
                    {
                        (Children[focused - 2] as NumberControl).SetFocus();
                    }
                    catch { }
                    OnEditElement(sender, null);
                }
            }
            else if (focused == 0 && GetFocusedStack() == this && GetFocusedStack() != Parent)      //редактирует ээлемент, превращая его из определеного элемента в элемент NumberControl
            {
                ResetElement();
            }

        }
        private void OnEditElement(object sender, EventArgs e)                              //метод, подписанный на событие, когда текст меняется. тут решается, какой будет вывод
        {
            try
            {
                Evaluate?.Invoke(this, Parent.Handler.Calculate().ToString());              //если получается посчитать выражение, то отправляем на вывод
            }
            catch (Exception ex)
            {
                Exception?.Invoke(this, ex.ToString());                  //если нет, то оставляем вывод пустым и вызываем событие ошибки для отладки
                Evaluate?.Invoke(this, "");
            }
        }

        public Stack(StackPanel panel, object instance)
        {
            Panel = panel; 
            Handler = new ElementsHandler();
            Reset();    //создает первоначальное графическое представление стека
            Instance = instance;
        }
        public void SetParent()                                                             //устанавливает родительский стек (главный стек)
        {
            Parent = this;
        }
        public void Reset()                                                                 //восстанавливает значение стека по умолчанию
        {
            Clear();
            var number = GetNewNumberControl();
            Children.Add(number);
            Handler.Elements.Add(number.Element);
            number.OnEdit += OnEditElement;
            number.OnDelete += OnDeleteElement;
            number.SetFocus();
        }

        public void ResetElement()                                                          //меняет элемент на элемент NumberControl
        {
            List<Stack> stacks = new List<Stack>();
            for (int i = 0; i < General.Count; i++) //ищем в ветках стеков элемент, который надо заменить на NumberControl
            {
                var stack = General[i];
                for (int j = 0; j < stack.Children.Count; j++)
                {
                    var searched = stack.Children[j];
                    if (searched == Instance)
                    {
                        stack.Children.RemoveAt(j);
                        stack.Handler.Elements.RemoveAt(j);
                        var number = GetNewNumberControl();
                        stack.Children.Insert(j, number);
                        stack.Handler.Elements.Insert(j, number.Element);
                        number.OnEdit += stack.OnEditElement;
                        number.OnDelete += stack.OnDeleteElement;
                        number.SetFocus();
                    }
                }
            }
        }

        public void Clear()                                                                 //очищает стек
        {
            Handler.Elements.Clear();
            Children.Clear();
            OnEditElement(this, null);
        }

        public int GetFocusedIndex()                                                        //получает индекс NumberControl, на котором установлен фокус
        {
            for (int i = 0; i < Children.Count; i++)
            {
                var element = Children[i];
                if (element is NumberControl && (element as NumberControl).Focused)
                {
                    return i;
                }
            }
            return -1;
        }
        public NumberControl GetFocusedElement()                                            //получает NumberControl, на котором установлен фокус
        {
            for (int i = 0; i < Children.Count; i++)
            {
                var element = Children[i];
                if (element is NumberControl && (element as NumberControl).Focused)
                {
                    return element as NumberControl;
                }
            }
            return null;
        }

        public void Input(string input)                                                     //Ввод значений с программной клавиатуры редактора формул, определяет, какое значение передается
                                                                                            //и на его основе добавляет элемент/элементы, либо заполняет цифрами, константами или точкой NumberControl, на котором установлен фокус
        {
            
            int focused = GetFocusedIndex();
            if (focused < 0)                       //если ни один элемент не имеет фокуса, то просто выходим из метода, потому что непонятно, куда и что добавлять без NumberControl с фокусом
                return;
            var alignment = VerticalAlignment.Center;
            if (IsPow)
                alignment = VerticalAlignment.Top;
            switch (input)
            {
                case "(":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var brackets = new BracketsControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, brackets);
                    Handler.Elements.Insert(focused, brackets.Element);
                    break;

                case "nsqrt":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var nsqrt = new NsqrtControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, nsqrt);
                    Handler.Elements.Insert(focused, nsqrt.Element);
                    break;
                case "sqrt":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var sqrt = new SqrtControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, sqrt);
                    Handler.Elements.Insert(focused, sqrt.Element);
                    break;
                case "divide": //деление дробью
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var divex = new DivideExtraControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, divex);
                    Handler.Elements.Insert(focused, divex.Element);
                    break;
                case "pow":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var pow = new PowControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, pow);
                    Handler.Elements.Insert(focused, pow.Element);
                    break;
                case "abs":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var abs = new AbsControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, abs);
                    Handler.Elements.Insert(focused, abs.Element);
                    break;

                case "/":
                    var divide = new DivideControl()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Children.Insert(focused + 1, divide);
                    Handler.Elements.Insert(focused + 1, divide.Operator);

                    var numberDivide = GetNewNumberControl();
                    numberDivide.OnEdit += OnEditElement;
                    numberDivide.OnDelete += OnDeleteElement;
                    Children.Insert(focused + 2, numberDivide);
                    numberDivide.TryFocus();
                    Handler.Elements.Insert(focused + 2, numberDivide.Element);
                    break;
                case "*":
                    var multiply = new MultiplyControl
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Children.Insert(focused + 1, multiply);
                    Handler.Elements.Insert(focused + 1, multiply.Operator);

                    var numberMulti = GetNewNumberControl();
                    numberMulti.OnEdit += OnEditElement;
                    numberMulti.OnDelete += OnDeleteElement;
                    Children.Insert(focused + 2, numberMulti);
                    numberMulti.TryFocus();
                    Handler.Elements.Insert(focused + 2, numberMulti.Element);
                    break;
                case "+":
                    var plus = new PlusControl
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Children.Insert(focused + 1, plus);
                    Handler.Elements.Insert(focused + 1, plus.Operator);

                    var numberPlus = GetNewNumberControl();
                    numberPlus.OnEdit += OnEditElement;
                    numberPlus.OnDelete += OnDeleteElement;
                    Children.Insert(focused + 2, numberPlus);
                    numberPlus.TryFocus();
                    Handler.Elements.Insert(focused + 2, numberPlus.Element);
                    break;
                case "-":
                    var minus = new MinusControl
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Children.Insert(focused + 1, minus);
                    Handler.Elements.Insert(focused + 1, minus.Operator);

                    var numberMinus = GetNewNumberControl();
                    numberMinus.OnEdit += OnEditElement;
                    numberMinus.OnDelete += OnDeleteElement;
                    Children.Insert(focused + 2, numberMinus);
                    numberMinus.TryFocus();
                    Handler.Elements.Insert(focused + 2, numberMinus.Element);
                    break;
                case "%":
                    var mod = new ModControl
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Children.Insert(focused + 1, mod);
                    Handler.Elements.Insert(focused + 1, mod.Operator);

                    var numberMod = GetNewNumberControl();
                    numberMod.OnEdit += OnEditElement;
                    numberMod.OnDelete += OnDeleteElement;
                    Children.Insert(focused + 2, numberMod);
                    numberMod.TryFocus();
                    Handler.Elements.Insert(focused + 2, numberMod.Element);
                    break;
                case ",":
                    (Children[focused] as NumberControl).Insert(".");
                    break;
                case "sin":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var sin = new SinControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, sin);
                    Handler.Elements.Insert(focused, sin.Element);
                    break;
                case "cos":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var cos = new CosControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, cos);
                    Handler.Elements.Insert(focused, cos.Element);
                    break;
                case "tan":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var tan = new TanControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, tan);
                    Handler.Elements.Insert(focused, tan.Element);
                    break;
                case "asin":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var asin = new ASinControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, asin);
                    Handler.Elements.Insert(focused, asin.Element);
                    break;
                case "acos":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var acos = new ACosControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, acos);
                    Handler.Elements.Insert(focused, acos.Element);
                    break;
                case "atan":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var atan = new ATanControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, atan);
                    Handler.Elements.Insert(focused, atan.Element);
                    break;

                case "log":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var log = new LogControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, log);
                    Handler.Elements.Insert(focused, log.Element);
                    break;
                case "ln":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var ln = new LnControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, ln);
                    Handler.Elements.Insert(focused, ln.Element);
                    break;

                case "deg":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var deg = new DegControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, deg);
                    Handler.Elements.Insert(focused, deg.Element);
                    break;
                case "PI":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var PI = GetNewNumberControl();
                    PI.OnEdit += OnEditElement;
                    PI.OnDelete += OnDeleteElement;
                    Children.Insert(focused, PI);
                    Handler.Elements.Insert(focused, PI.Element);
                    PI.SetFocus();
                    PI.SetConstant(Enums.Constants.Pi);
                    break;

                case "e":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var ex = GetNewNumberControl();
                    ex.OnEdit += OnEditElement;
                    ex.OnDelete += OnDeleteElement;
                    Children.Insert(focused, ex);
                    Handler.Elements.Insert(focused, ex.Element);
                    ex.SetFocus();
                    ex.SetConstant(Enums.Constants.E);
                    break;
                case "Phi":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var phi = GetNewNumberControl();
                    phi.OnEdit += OnEditElement;
                    phi.OnDelete += OnDeleteElement;
                    Children.Insert(focused, phi);
                    Handler.Elements.Insert(focused, phi.Element);
                    phi.SetFocus();
                    phi.SetConstant(Enums.Constants.Phi);
                    break;
                case "round":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var round = new RoundControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, round);
                    Handler.Elements.Insert(focused, round.Element);
                    break;
                case "ceil":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var ceil = new CeilControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, ceil);
                    Handler.Elements.Insert(focused, ceil.Element);
                    break;
                case "floor":
                    Children.RemoveAt(focused);
                    Handler.Elements.RemoveAt(focused);
                    var floor = new FloorControl()
                    {
                        VerticalAlignment = alignment,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MinHeight = MinHeight,
                        MinWidth = MinWidth,
                    };
                    Children.Insert(focused, floor);
                    Handler.Elements.Insert(focused, floor.Element);
                    break;
                case "1":
                    (Children[focused] as NumberControl).Insert("1");
                    break;
                case "2":
                    (Children[focused] as NumberControl).Insert("2");
                    break;
                case "3":
                    (Children[focused] as NumberControl).Insert("3");
                    break;
                case "4":
                    (Children[focused] as NumberControl).Insert("4");
                    break;
                case "5":
                    (Children[focused] as NumberControl).Insert("5");
                    break;
                case "6":
                    (Children[focused] as NumberControl).Insert("6");
                    break;
                case "7":
                    (Children[focused] as NumberControl).Insert("7");
                    break;
                case "8":
                    (Children[focused] as NumberControl).Insert("8");
                    break;
                case "9":
                    (Children[focused] as NumberControl).Insert("9");
                    break;
                case "0":
                    (Children[focused] as NumberControl).Insert("0");
                    break;
            }
        }

        public NumberControl GetNewNumberControl()                                          //Возвращает новый NumberControl, который соответствует степенному, либо обычному виду
        {
            var alignment = VerticalAlignment.Center;
            if (IsPow)
                alignment = VerticalAlignment.Top;
            var ctrl = new NumberControl
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = alignment,
                MinHeight = MinHeight,
                MinWidth = MinWidth
            };
            return ctrl;
        }
        public static Stack GetFocusedStack()                                               //Получает стек, в котором на одном из элементов NumberControl есть фокус
        {
            foreach (var stack in General)
            {
                if (stack.StackIsFocused)
                    return stack;
            }
            return null;
        }
        public static Stack AddStack(Stack stack)                                           //Добавляет стек в общий список стеков
        {
            General.Add(stack);
            return stack;
        }
        public static void RemoveStack(Stack stack)                                         //удаляет стек
        {
            stack.Clear();
            General.Remove(stack);
        }
    }
}
