using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MathUtils;

namespace SolvingMethods
{
    public class DFSAppendSolver : AppendSolver
    {
        public DFSAppendSolver()
        {

        }

        public DFSAppendSolver(string[] numbers, Op op) : base(numbers, op)
        {

        }

        protected override bool AppendSolve(string digits)
        {
            if (ct.IsCancellationRequested)
            {
                End();
                throw new TaskCanceledException();
            }
            if (varString.Length == digits.Length)
            {

                if (!Satisfied(digits))
                    return false;
                else
                {
                    results.Add(digits);
                    return true;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (digits.Contains(i.ToString()))
                    continue;
                string newDigits = digits + i.ToString();
                if (AppendSolve(newDigits) && !findAll)
                    return true;
            }
            return false;
        }
    }
}
