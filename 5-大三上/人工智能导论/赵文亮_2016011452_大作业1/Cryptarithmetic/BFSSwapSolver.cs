using System;
using MathUtils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolvingMethods
{
    public class BFSSwapSolver : SwapSolver
    {
        private Queue<char[]> bfs_queue = new Queue<char[]>();
        public BFSSwapSolver()
        {
        }

        public BFSSwapSolver(string[] numbers, Op op) : base(numbers, op)
        {
        }

        public override bool SwapSolve(char[] ca, int depth = 0)
        {
            bfs_queue.Clear();
            bfs_queue.Enqueue(ca);
            bfs_queue.Enqueue(null);// to detect the depth
            while (bfs_queue.Count != 0)
            {

                char[] cur_ca = bfs_queue.Dequeue();
                if (cur_ca == null)// end of level
                {
                    depth++;
                    if (depth > varString.Length)
                    {
                        break;
                    }
                    bfs_queue.Enqueue(null);
                    continue;
                }
                // new nodes
                for (int i = 0; i < 10; i++)
                {
                    for (int j = i + 1; j < 10; j++)
                    {
                        // swap i, j
                        if (!checkValid(i, j, ref ca))
                            continue;
                        char[] newCa = new char[10];
                        cur_ca.CopyTo(newCa, 0);
                        newCa[i] = ca[j];
                        newCa[j] = ca[i];
                        if (closeList.ContainsKey(newCa))
                            continue;
                        addToCloseList(newCa);
                        refreshHash(newCa);
                        int loss = CalcLoss();
                        if (loss == 0)
                            return true;
                        InterruptCheck(depth);
                        bfs_queue.Enqueue(newCa);
                        refreshHash(ca);
                    }
                }
            }
            return false;
        }
    }
}
