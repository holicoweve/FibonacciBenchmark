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
        public delegate long Fibonacci(int n);

        static void Main(string[] args)
        {
            string[] input;
            int n = 500;
            long result;
            Stopwatch sw = new Stopwatch();
            Fibonacci generator = null;

            while (true)
            {
                PrintMenu(n);
                input = Console.ReadLine().Split(' ');
                sw.Reset();

                switch (input[0])
                {
                    case "1":
                        generator = FibonacciClassic.Fibonacci;
                        break;
                    case "2":
                        generator = FibonacciArray.Fibonacci;
                        break;
                    case "n":
                        Int32.TryParse(input[1], out n);
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
                    sw.Start();
                    result = generator(n);
                    sw.Stop();
                    Console.WriteLine($"Fibonacci({n}) = {result:N0}");
                    Console.WriteLine($"Time spend = {sw.ElapsedMilliseconds:N0} ms");
                    generator = null;
                }
            }

        }

        static void PrintMenu(int n)
        {
            Console.WriteLine();
            Console.WriteLine("FibonacciBenchmark");
            Console.WriteLine("  1 Classic Fibonacci generator");
            Console.WriteLine("  2 Array Fibonacci generator");
            Console.WriteLine($"  n Calculate nth Fibonacci number (Current value {n})");
            Console.WriteLine("  q Quit");
            Console.Write(">");
        }
    }

    public static class FibonacciClassic
    {
        public static long Fibonacci(int n)
        {
            if (n <= 0)
                return 0;
            else if (n == 1)
                return 1;
            else
                try
                {
                    return checked(Fibonacci(n - 2) + Fibonacci(n - 1));
                }
                catch (OverflowException e)
                {
                    Console.WriteLine($"Calculation overflow occured at n={n}");
                    return 0;
                }

        }
    }

    public static class FibonacciArray
    {
        public static long Fibonacci(int n)
        {
            long prevprev = 0;
            long prev = 1;
            long[] array;

            if (n <= 0)
                return prevprev;
            else if (n == 1)
                return prev;
            else
            {
                array = new long[n + 1];
                array[0] = prevprev;
                array[1] = prev;
                for (int i = 2; i < array.Length; i++)
                {
                    try
                    {
                        array[i] = checked(array[i - 1] + array[i - 2]);
                    }
                    catch (OverflowException e)
                    {
                        Console.WriteLine($"Calculation overflow occured at n={i}");
                        return 0;
                    }
                }
                return array[n];
            }
        }
    }
}
