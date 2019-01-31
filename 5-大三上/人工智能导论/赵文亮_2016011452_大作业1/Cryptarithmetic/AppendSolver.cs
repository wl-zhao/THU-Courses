using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MathUtils;

namespace SolvingMethods
{
    public abstract class AppendSolver : Solver
    {
        protected CancellationToken ct;
        public AppendSolver()
        {

        }

        public AppendSolver(string[] numbers, Op op) : base(numbers, op)
        {

        }
        public override void Solve(bool findAll = false)
        {
            this.findAll = findAll;
            AppendSolve("");
        }

        public override void Solve(CancellationToken ct)
        {
            this.ct = ct;
            Solve();
            End();
        }

        public override void Solve()
        {
            Solve(false);
        }

        public override void End()
        {
            char2digit.Clear();
            Console.WriteLine("Append End");
            GC.Collect(10);
        }

        protected abstract bool AppendSolve(string digits);

        protected int calcLoss(string digits)
        {
            int res = 0;
            switch (op)
            {
                case Op.PLUS:
                    res = 0;
                    foreach (string Operand in Operands)
                    {
                        res += (int)SubstituteWithDigits(Operand, digits);
                    }
                    break;
                case Op.MINUS:
                    res = (int)SubstituteWithDigits(Operands[0], digits) - (int)SubstituteWithDigits(Operands[1], digits);
                    break;
                case Op.MULTIPLY:
                    res = 1;
                    foreach (string Operand in Operands)
                    {
                        res *= (int)SubstituteWithDigits(Operand, digits);
                    }
                    break;
                case Op.DIVIDE:
                    res = (int)SubstituteWithDigits(Operands[1], digits) * (int)SubstituteWithDigits(Answer, digits);
                    return Math.Abs(res - (int)SubstituteWithDigits(Operands[0], digits));
                default:
                    break;
            }
            return Math.Abs(res - (int)SubstituteWithDigits(Answer, digits));
        }

        protected bool Satisfied(string digits)
        {
            return calcLoss(digits) == 0;
        }

        protected void ListResults()
        {
            foreach (string result in results)
            {
                printResult(result);
            }
        }

        public override string[] GetResult()
        {
            if (results.Count == 0)
                throw new Exception(EXC_MESSAGE[0]);
            string[] result = new string[Numbers.Length];
            for (int i = 0; i < Numbers.Length; i++)
            {
                result[i] = (string)SubstituteWithDigits(Numbers[i],
                    results[new Random().Next(results.Count)], true);
            }
            return result;
        }
    }
}
