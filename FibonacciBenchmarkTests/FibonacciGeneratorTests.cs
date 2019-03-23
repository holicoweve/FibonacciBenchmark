using FibonacciBenchmark;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciBenchmarkTests
{
    [TestClass]
    public abstract class FibonacciBaseTests
    {
        protected abstract IFibonacciGenerator GetGeneratorInstance();

        [DataTestMethod]
        [DataRow(0UL,0)]
        [DataRow(1UL,1)]
        [DataRow(1UL,2)]
        [DataRow(21UL,8)]
        [DataRow(34UL,9)]
        [DataRow(55UL,10)]
        public void PositiveInputTest(ulong output, int input)
        {
            var generator = GetGeneratorInstance();
            Assert.AreEqual(output, generator.Fibonacci(input));

        }
    }

    [TestClass]
    public class FibonacciArrayTest : FibonacciBaseTests
    {
        protected override IFibonacciGenerator GetGeneratorInstance()
        {
            return new FibonacciArray();
        }
    }

    [TestClass]
    public class FibonacciIterationTest : FibonacciBaseTests
    {
        protected override IFibonacciGenerator GetGeneratorInstance()
        {
            return new FibonacciIteration();
        }
    }

    [TestClass]
    public class FibonacciClassicTest : FibonacciBaseTests
    {
        protected override IFibonacciGenerator GetGeneratorInstance()
        {
            return new FibonacciClassic();
        }
    }
}