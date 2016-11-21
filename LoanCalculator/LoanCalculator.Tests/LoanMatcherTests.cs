using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoanCalculator.Tests
{
    [TestClass]
    public class LoanMatcherTests : TestsBase
    {
        private ILoanMatcher _matcher;

        [TestInitialize]
        public void Init()
        {
            _matcher = new LoanMatcher();
        }

        [TestMethod]
        public void ExactAmountMatchingReturnsOneLender()
        {
            var listLenders = LenderList.OrderBy(x => x.Rate).ToList();

            var result = _matcher.Match(480, listLenders);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(480, result.First().Available);
            Assert.AreEqual(0.069, result.First().Rate);
            Assert.AreEqual("Jane", result.First().Name);
        }

        [TestMethod]
        public void PrincipalWithoutExactMatchReturnsOneWholeandOnePartialMatch()
        {
            var listLenders = LenderList.OrderBy(x => x.Rate).ToList();
            var result = _matcher.Match(640, listLenders);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void ExactTotalReturnsAllLenders()
        {
            var listLenders = LenderList.OrderBy(x => x.Rate).ToList();
            var result = _matcher.Match(2330, listLenders);
            Assert.AreEqual(7, result.Count);
        }
    }
}