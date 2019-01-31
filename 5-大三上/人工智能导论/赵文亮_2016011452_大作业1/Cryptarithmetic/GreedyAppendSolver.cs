using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MathUtils;

namespace SolvingMethods
{
    public class GreedyAppendSolver : AppendSolver
    {
        public GreedyAppendSolver()
        {

        }

        public GreedyAppendSolver(string[] numbers, Op op) : base(numbers, op)
        {

        }

        /// <summary>
        /// Greedy append solve method
        /// </summary>
        /// <param name="digits">current state</param>
        /// <returns></returns>
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

            List<int> lossList = new List<int>();
            List<string> stringList = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                if (digits.Contains(i.ToString()))
                    continue;
                string newDigits = digits + i.ToString();
                stringList.Add(newDigits);
                lossList.Add(calcLoss(newDigits));
            }
            var sorted = lossList.Select((x, i) => new KeyValuePair<int, int>(x, i)).OrderBy(x => x.Key).ToList();
            var idx = sorted.Select(x => x.Value).ToList();
            foreach (int i in idx)
            {
                if (AppendSolve(stringList[i]) && !findAll)
                    return true;
            }
            return results.Any();
        }
    }
}
