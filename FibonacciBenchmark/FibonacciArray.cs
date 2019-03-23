using System;

namespace FibonacciBenchmark
{
    public class FibonacciArray : IFibonacciGenerator
    {
        public string Name()
        {
            return "Array Fibonacci Generator";
        }

        public ulong Fibonacci(int n)
        {
            ulong[] array;

            if (n == 0)
                return 0UL;
            else if (n == 1)
                return 1UL;
            else
            {
                array = new ulong[n + 1];
                array[0] = 0L;
                array[1] = 1L;
                for (int i = 2; i < array.Length; i++)
                {
                    try
                    {
                        array[i] = checked(array[i - 1] + array[i - 2]);
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine($"Calculation overflow occured at n={i}");
                        throw;
                    }
                }

                return array[n];
            }
        }
    }
}