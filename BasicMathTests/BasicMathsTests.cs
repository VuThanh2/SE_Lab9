using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicMath;
using System;

namespace BasicMathTests
{
    [TestClass]
    public class BasicMathsTests
    {
        [DataTestMethod]
        [DataRow(1, 1, 2)]          // EP: Positive numbers
        [DataRow(-1, -1, -2)]       // EP: Negative numbers
        [DataRow(0, 0, 0)]          // EP: Zero
        [DataRow(int.MaxValue, 1, (double)int.MaxValue + 1)] // BVA: Upper boundary
        [DataRow(int.MinValue, -1, (double)int.MinValue - 1)] // BVA: Lower boundary
        public void Test_AddMV(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(5, 3, 2)]          // EP: Positive numbers
        [DataRow(-5, -3, -2)]       // EP: Negative numbers
        [DataRow(0, 0, 0)]          // EP: Zero
        public void Test_SubtractMV(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(10, 2, 5)]         // EP: Positive numbers
        [DataRow(-10, -2, 5)]       // EP: Negative numbers
        [DataRow(0, 1, 0)]          // EP: Zero numerator
        public void Test_DivideMV(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Divide(a, b);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Test_DivideByZero_ThrowsException()
        {
            BasicMaths bm = new BasicMaths();
            bm.Divide(10, 0);
        }

        [DataTestMethod]
        [DataRow(2, 3, 6)]          // EP: Positive numbers
        [DataRow(-2, -3, 6)]        // EP: Negative numbers
        [DataRow(0, 5, 0)]          // EP: Zero
        public void Test_MultiplyMV(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Multiply(a, b);
            Assert.AreEqual(expected, actual);
        }
    }
}