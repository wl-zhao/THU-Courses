using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using MathUtils;

namespace SolvingMethods
{
    abstract public class Solver
    {
        protected string[] EXC_MESSAGE = { "无解", "无效输入" };
        protected Op op;

        protected const string Operators = "+-×÷";
        protected string varString = "";
        protected Hashtable char2digit = new Hashtable();
        protected List<string> results = new List<string>();

        protected bool findAll = false;

        protected string[] Numbers { get; set; }
        protected string Answer;
        protected string[] Operands;

        public Solver()
        {

        }

        public Solver(string []numbers, Op op)
        {
            Init(numbers, op);
        }

        public void Init(string []numbers, Op op)
        {
            Numbers = numbers;
            Answer = Numbers.Last();
            Operands = Numbers.Take(Numbers.Length - 1).ToArray();
            this.op = op;
            InitSolve();
        }

        public abstract void End();

        public abstract string[] GetResult();


        protected virtual void InitSolve()
        {
            results.Clear();
            ExtractVariables();
            BasicCheck();
        }

        abstract public void Solve(bool findAll);
        abstract public void Solve();
        abstract public void Solve(CancellationToken e);

        protected object SubstituteWithDigits(string number, string digits, bool toString = false)
        {
            char c;
            for (int i = 0; i < varString.Length; i++)
            {
                c = i < digits.Length ? digits[i] : '5';
                number = number.Replace(varString[i], c);
            }
            if (toString)
                return number;
            else
                return int.Parse(number);
        }

        protected object SubstituteWithDigits(string number, bool toString = false)
        {
            foreach (char var in varString)
            {
                number = number.Replace(var, (char)((int)char2digit[var] + '0'));
            }
            if (toString)
                return number;
            else
                return int.Parse(number);
        }

        public void print()
        {
            for (int i = 0; i < Numbers.Length; i++)
            {
                if (i < Numbers.Length - 2)
                {
                    Console.WriteLine("{0, 20}", Numbers[i]);
                }
                else if (i < Numbers.Length - 1)
                {
                    Console.WriteLine("{0,-10}{1,10}", Operators[(int)op], Numbers[i]);
                }
                else
                {
                    for (int j = 0; j < 20; j++)
                        Console.Write("-");
                    Console.WriteLine("\n{0,20}", Numbers[i]);
                }
            }
        }

        public void printResult(string digits, params object[] args)
        {
            Console.WriteLine(varString + "->" + digits);
            for (int i = 0; i < Numbers.Length; i++)
            {
                string content = (string)SubstituteWithDigits(Numbers[i], digits, true);
                if (i < Numbers.Length - 2)
                {
                    Console.WriteLine("{0, 20}", content);
                }
                else if (i < Numbers.Length - 1)
                {
                    Console.WriteLine("{0,-10}{1,10}", Operators[(int)op], content);
                }
                else
                {
                    for (int j = 0; j < 20; j++)
                        Console.Write("-");
                    Console.WriteLine("\n{0,20}", content);
                }
            }
        }

        private void ExtractVariables(bool show = false)
        {
            char2digit.Clear();
            varString = "";
            int count = 0;
            foreach (string num in Numbers)
            {
                foreach (char c in num)
                {
                    if (c > 127)
                    {
                        throw new Exception(EXC_MESSAGE[1] + ":包含无效字符");
                    }

                    if (!char.IsDigit(c) && !char2digit.ContainsKey(c))
                    {
                        char2digit.Add(c, count++);
                        varString = varString + c.ToString();
                    }
                }
            }
            
            if (char2digit.Count > 10)
            {
                throw new Exception(EXC_MESSAGE[1] + ":变量数目大于10");
            }

            if (show)
            {
                foreach (char c in varString)
                {
                    Console.Write(c);
                }
                Console.Write("\n");
            }
        }


        private void BasicCheck()
        {
            if (Answer.Length == 0)
                throw new Exception(EXC_MESSAGE[1]);
            int max_length;
            switch (op)
            {
                case Op.PLUS:
                    max_length = Operands.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;
                    if (max_length < Answer.Length - 1 || max_length > Answer.Length)
                        throw new Exception(EXC_MESSAGE[1]);
                    break;
                case Op.MINUS:
                    max_length = Operands.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;
                    if (max_length < Answer.Length || max_length > Answer.Length + 1)
                        throw new Exception(EXC_MESSAGE[1]);
                    break;
                case Op.MULTIPLY:
                    max_length = 1;
                    foreach (string Operand in Operands)
                    {
                        max_length *= Operand.Length;
                    }
                    if (max_length < Answer.Length)
                        throw new Exception(EXC_MESSAGE[1]);
                    break;
                case Op.DIVIDE:
                    if (Operands[0].Length > Operands[1].Length * Answer.Length)
                        throw new Exception(EXC_MESSAGE[1]);
                    break;
                default:
                    break;
            }
        }
    }
}
