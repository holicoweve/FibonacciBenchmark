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
        private delegate ulong Fibonacci(int n);

        static void Main(string[] args)
        {
            var n = 30;
            var sw = new Stopwatch();
            IFibonacciGenerator generator = null;

            while (true)
            {
                PrintMenu(n);
                var input = Console.ReadLine().Split(' ');

                switch (input[0])
                {
                    case "1":
                        generator = new FibonacciClassic();
                        break;
                    case "2":
                        generator = new FibonacciArray();
                        break;
                    case "3":
                        generator = new FibonacciIteration();
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
                    var result = 0UL;
                    try
                    {
                        result = generator.Fibonacci(n);
                    }
                    catch (OverflowException)
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
}