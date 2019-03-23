using System;

namespace FibonacciBenchmark
{
    public class FibonacciClassic : IFibonacciGenerator
    {
        public string Name()
        {
            return "Classic Fibonacci Generator";
        }

        public ulong Fibonacci(int n)
        {
            if (n == 0)
                return 0UL;
            else if (n == 1)
                return 1UL;
            else
                try
                {
                    return checked(Fibonacci(n - 2) + Fibonacci(n - 1));
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"Calculation overflow occured at n={n}");
                    throw;
                }
        }
    }
}