using System;

namespace FibonacciBenchmark
{
    public class FibonacciIteration : IFibonacciGenerator
    {
        public string Name()
        {
            return "Iterative Fibonacci Generator";
        }

        public ulong Fibonacci(int n)
        {
            if (n == 0)
                return 0UL;
            else if (n == 1)
                return 1UL;
            else
            {
                var prev = 1UL;
                var prevprev = 0UL;
                var result = 1UL;
                var i = 1;
                while (i < n)
                {
                    try
                    {
                        result = checked(prev + prevprev);
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine($"Calculation overflow occured at n={i}");
                        throw;
                    }

                    prevprev = prev;
                    prev = result;
                    i++;
                }

                return result;
            }
        }
    }
}