namespace FibonacciBenchmark
{
    public interface IFibonacciGenerator
    {
        string Name();
        ulong Fibonacci(int n);
    }
}