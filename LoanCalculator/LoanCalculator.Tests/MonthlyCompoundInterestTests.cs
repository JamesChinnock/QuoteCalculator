using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoanCalculator.Tests
{
    [TestClass]
    public class ThirtySixMonthCompoundInterestTests
    {
        ICompoundStrategy _strategy = new ThirtySixMonthStrategy();

        [TestMethod]
        public void InitializeNewInstance()
        {
            _strategy = new ThirtySixMonthStrategy();   
        }

        [TestMethod]
        public void CompoundInterestReturnsCorrect()
        {
            var result = _strategy.CompoundInterest(1500, 0.043);
            var expected = ExpectedResult(1500, 0.043);

            Assert.AreEqual(expected, result);
        }


        private double ExpectedResult(double principal, double interestRate)
        {
            // Annual = Principal (1 + rate/times-per-year) to power of [times-per-year*years]

            var body = (1 + interestRate/12);
            var exp = 12*3;
            var annual = principal * Math.Pow(body, exp);

            return annual;
        }

    }
}