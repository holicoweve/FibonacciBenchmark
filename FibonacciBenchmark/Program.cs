using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FibonacciBenchmark
{
    public class Program
    {
        private static readonly List<IFibonacciGenerator> _generators = new List<IFibonacciGenerator>
            {new FibonacciClassic(), new FibonacciArray(), new FibonacciIteration()};

        static void Main(string[] args)
        {
            var defaultInput = 30;
            var sw = new Stopwatch();
            IFibonacciGenerator generator = null;

            while (true)
            {
                PrintMenu(defaultInput);
                var input = Console.ReadLine().Split(' ');

                switch (input[0])
                {
                    case "n":
                        if (input.Length == 1 || !Int32.TryParse(input[1], out defaultInput))
                        {
                            Console.WriteLine("Syntax Error: to set n to 30 please use the following example");
                            Console.WriteLine("n 30");
                            break;
                        }

                        if (defaultInput < 0)
                        {
                            Console.WriteLine("Please enter only positive n");
                            defaultInput = 1;
                        }

                        break;
                    case "exit":
                    case "q":
                        return;
                    default:
                        generator = GetGenerator(input[0]);
                        break;
                }

                if (generator != null)
                {
                    sw.Reset();
                    sw.Start();
                    var result = 0UL;
                    try
                    {
                        result = generator.Fibonacci(defaultInput);
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
                    Console.WriteLine($"Fibonacci({defaultInput}) = {result:N0}");
                    Console.WriteLine($"Time spend = {sw.ElapsedMilliseconds:N0} ms");
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }

        public static IFibonacciGenerator GetGenerator(string choiceString)
        {
            if (Int32.TryParse(choiceString, out int choice))
                return GetGenerator(choice);
            else
                return null;
        }

        public static IFibonacciGenerator GetGenerator(int choice)
        {
            if (choice >= 1 && choice <= _generators.Count)
                return _generators[choice - 1];
            else
                return null;
        }

        static void PrintMenu(int n)
        {
            Console.WriteLine();
            Console.WriteLine("FibonacciBenchmark");
            for (int i = 0; i < _generators.Count; i++)
            {
                Console.WriteLine($"  {i + 1} {_generators[i].Name()}");
            }

            Console.WriteLine($"  n Calculate nth Fibonacci number (Current n={n})");
            Console.WriteLine("  q Quit");
            Console.Write(">");
        }
    }
}