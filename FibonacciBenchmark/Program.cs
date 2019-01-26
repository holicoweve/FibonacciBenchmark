using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FibonacciBenchmark
{
    class Program
    {
        public delegate ulong Fibonacci(int n);

        static void Main(string[] args)
        {
            string[] input;
            int n = 30;
            ulong result;
            Stopwatch sw = new Stopwatch();
            Fibonacci generator = null;

            while (true)
            {
                PrintMenu(n);
                input = Console.ReadLine().Split(' ');

                switch (input[0])
                {
                    case "1":
                        generator = FibonacciClassic.Fibonacci;
                        break;
                    case "2":
                        generator = FibonacciArray.Fibonacci;
                        break;
                    case "3":
                        generator = FibonacciIteration.Fibonacci;
                        break;
                    case "n":
                        if (input.Length == 1 || !Int32.TryParse(input[1], out n))
                        {
                            Console.WriteLine("Syntax Error: to set n to 30 please use the following example");
                            Console.WriteLine("n 30");
                            break;
                        }
                        if (n < 0)
                        {
                            Console.WriteLine("Please enter only positive n");
                            n = 1;
                        }
                        break;
                    case "exit":
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }

                if (generator != null)
                {
                    sw.Reset();
                    sw.Start();
                    try
                    {
                        result = generator(n);
                    }
                    catch (OverflowException e)
                    {
                        continue;
                    }
                    finally
                    {
                        generator = null;
                    }
                    sw.Stop();
                    Console.WriteLine($"Fibonacci({n}) = {result:N0}");
                    Console.WriteLine($"Time spend = {sw.ElapsedMilliseconds:N0} ms");
                }
            }
        }

        static void PrintMenu(int n)
        {
            Console.WriteLine();
            Console.WriteLine("FibonacciBenchmark");
            Console.WriteLine("  1 Classic recursion");
            Console.WriteLine("  2 Utilizing an Array");
            Console.WriteLine("  3 Iteration");
            Console.WriteLine($"  n Calculate nth Fibonacci number (Current n={n})");
            Console.WriteLine("  q Quit");
            Console.Write(">");
        }
    }

    public static class FibonacciClassic
    {
        public static ulong Fibonacci(int n)
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
                catch (OverflowException e)
                {
                    Console.WriteLine($"Calculation overflow occured at n={n}");
                    throw;
                }
        }
    }

    public static class FibonacciArray
    {
        public static ulong Fibonacci(int n)
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
                    catch (OverflowException e)
                    {
                        Console.WriteLine($"Calculation overflow occured at n={i}");
                        throw;
                    }
                }
                return array[n];
            }
        }
    }

    public static class FibonacciIteration
    {
        public static ulong Fibonacci(int n)
        {
            if (n == 0)
                return 0UL;
            else if (n == 1)
                return 1UL;
            else
            {
                ulong prev = 1UL;
                ulong prevprev = 0UL;
                ulong result = 1UL;
                int i = 1;
                while (i < n)
                {
                    try
                    {
                        result = checked(prev + prevprev);
                    }
                    catch (OverflowException e)
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
