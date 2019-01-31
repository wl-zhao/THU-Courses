using System;
using MathUtils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolvingMethods
{
    public class DFSSwapSolver : SwapSolver
    {
        public DFSSwapSolver()
        {
        }

        public DFSSwapSolver(string[] numbers, Op op) : base(numbers, op)
        {
        }

        public override bool SwapSolve(char[] ca, int depth = 0)
        {
            if (!InterruptCheck(depth))
            {
                return false;
            }

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

                    try
                    {
                        if (SwapSolve(newCa, depth + 1))
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    refreshHash(ca);
                }
            }
            return false;
        }
    }
}
