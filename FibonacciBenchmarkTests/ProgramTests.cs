using Microsoft.VisualStudio.TestTools.UnitTesting;
using FibonacciBenchmark;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciBenchmarkTests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void GetGeneratorOutOfBoundTest()
        {
            Assert.IsNull(Program.GetGenerator(-1));
            Assert.IsNull(Program.GetGenerator(0));
            Assert.IsNull(Program.GetGenerator("4"));

        }

        [TestMethod]
        public void GetGeneratorCorrectReturnTest()
        {
            Assert.IsInstanceOfType(Program.GetGenerator("1"),typeof(FibonacciClassic));
            Assert.IsInstanceOfType(Program.GetGenerator(2),typeof(FibonacciArray));
            Assert.IsInstanceOfType(Program.GetGenerator(3),typeof(FibonacciIteration));
        }
    }
}