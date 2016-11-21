using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoanCalculator.Tests
{
    [TestClass]
    public class CsvRepositoryIntegrationTests
    {
        [TestMethod]
        public void BasictestToReturnDataFromEmbeddedResource()
        {
            CsvRepository r = new CsvRepository("market.csv");
            var res = r.RetrieveAvailableLenders();
            Assert.AreEqual(7, res.Count);
        }
    }
}