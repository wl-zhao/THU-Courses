using System;
using MathUtils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolvingMethods
{
    public class GreedySwapSolver : SwapSolver
    {
        public GreedySwapSolver()
        {
        }

        public GreedySwapSolver(string[] numbers, Op op) : base(numbers, op)
        {
        }

        public override bool SwapSolve(char[] ca, int depth = 0)
        {
            if (!InterruptCheck(depth))
            {
                return false;
            }

            List<char[]> caList = new List<char[]>();
            List<int> lossList = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = i + 1; j < 10; j++)
                {
                    // swap i, j
                    if (!checkValid(i, j, ref ca))
                        continue;
                    char[] newCa = new char[10];
                    ca.CopyTo(newCa, 0);
                    newCa[i] = ca[j];
                    newCa[j] = ca[i];
                    if (closeList.ContainsKey(newCa))
                        continue;
                    addToCloseList(newCa);
                    refreshHash(newCa);
                    int loss = CalcLoss();
                    if (loss == 0)
                        return true;
                    caList.Add(newCa);
                    lossList.Add(loss);
                    refreshHash(ca);
                }
            }

            var sorted = lossList.Select((x, i) => new KeyValuePair<int, int>(x, i)).OrderBy(x => x.Key).ToList();
            var idx = sorted.Select(x => x.Value).ToList();
            foreach (int i in idx)
            {
                if (SwapSolve(caList[i], depth + 1))
                    return true;
            }
            return false;
        }
    }
}
