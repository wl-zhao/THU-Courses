using System;
using MathUtils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SolvingMethods
{
    public abstract class SwapSolver : Solver
    {
        public static int currentDepth;
        protected int Steps;
        protected int MAX_STEPS = 4000000;
        protected Hashtable closeList = new Hashtable();
        protected char[] varsArray;
        protected bool has_solution = false;
        protected CancellationToken ct;

        public SwapSolver()
        {
        }

        public SwapSolver(string[] numbers, Op op) : base(numbers, op)
        {
        }

        public override void Solve(bool findAll = false)
        {
            Steps = 0;
            closeList.Clear();
            fillVars();
            refreshHash(varsArray);
            if (CalcLoss() == 0)
            {
                has_solution = true;
                return;
            }
            try
            {
                has_solution = SwapSolve(varsArray);
            }
            catch (Exception)
            {
                results.Clear();
            }
            End();
        }
        public override void Solve(CancellationToken ct)
        {
            this.ct = ct;
            Solve();
        }

        protected override void InitSolve()
        {
            base.InitSolve();
            has_solution = false;
        }

        public override void End()
        {
            closeList.Clear();
            Console.WriteLine("Swap End");
            GC.Collect(10);
        }

        public override void Solve()
        {
            Solve(false);
        }

        public abstract bool SwapSolve(char[] ca, int depth = 0);

        /// <summary>
        /// check if the swap is valid
        /// </summary>
        /// <param name="i">first index</param>
        /// <param name="j">second index</param>
        /// <param name="ca">char array</param>
        /// <returns></returns>
        protected bool checkValid(int i, int j, ref char []ca)
        {
            return (ca[i] != ca[j]);
        }

        /// <summary>
        /// fill the varString with '\0'
        /// </summary>
        protected void fillVars()
        {
            string filledVarString = varString;
            for (int i = varString.Length; i < 10; i++)
            {
                filledVarString = filledVarString + "\0";
            }
            varsArray = filledVarString.ToCharArray();
            addToCloseList(varsArray);
        }

        protected void addToCloseList(char[] ca)
        {
            if (!closeList.ContainsKey(ca))
                closeList.Add(ca, -1);
        }

        protected string getDigits()
        {
            string digits = varString;
            foreach (char c in char2digit.Keys)
            {
                digits = digits.Replace(c, (char)((int)char2digit[c] + '0'));
            }
            return digits;
        }

        /// <summary>
        /// update char2digit according to ca
        /// </summary>
        /// <param name="ca">Chararray</param>
        protected void refreshHash(char[] ca)
        {
            for (int i = 0; i < 10; i++)
            {
                if (char2digit.ContainsKey(ca[i]))
                {
                    char2digit[ca[i]] = i;
                }
            }
        }

        public override string[] GetResult()
        {
            if (!has_solution)
                throw new Exception(EXC_MESSAGE[0]);
            string[] result = new string[Numbers.Length];
            for (int i = 0; i < Numbers.Length; i++)
            {
                result[i] = (string)SubstituteWithDigits(Numbers[i], true);
            }
            return result;
        }

        protected int CalcLoss()
        {
            int res = 0;
            switch (op)
            {
                case Op.PLUS:
                    res = 0;
                    foreach (string Operand in Operands)
                    {
                        res += (int)SubstituteWithDigits(Operand);
                    }
                    break;
                case Op.MINUS:
                    res = (int)SubstituteWithDigits(Operands[0]) - (int)SubstituteWithDigits(Operands[1]);
                    break;
                case Op.MULTIPLY:
                    res = 1;
                    foreach (string Operand in Operands)
                    {
                        res *= (int)SubstituteWithDigits(Operand);
                    }
                    break;
                case Op.DIVIDE:
                    res = (int)SubstituteWithDigits(Operands[1]) * (int)SubstituteWithDigits(Answer);
                    return Math.Abs(res - (int)SubstituteWithDigits(Operands[0]));
                default:
                    break;
            }
            return Math.Abs(res - (int)SubstituteWithDigits(Answer));
        }

        protected bool InterruptCheck(int depth)
        {
            if (ct.IsCancellationRequested)
            {
                End();
                Console.WriteLine("End By Cancel");
                throw new TaskCanceledException();
            }
            if (Steps++ > MAX_STEPS)
            {
                End();
                Console.WriteLine("End By Timeout");
                throw new Exception("Timeout");
            }
            if (depth > varString.Length)
            {
                return false;
            }
            return true;
        }
    }
}
