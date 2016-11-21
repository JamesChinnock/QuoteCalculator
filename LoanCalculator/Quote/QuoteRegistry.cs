using LoanCalculator;
using StructureMap;

namespace Quote
{
    public class QuoteRegistry : Registry
    {
        public Container GetContainer(string path)
        {
            return new Container(x =>
            {
                x.For<ICalculator>().Use<Calculator>();
                x.For<ICompoundStrategy>().Use<ThirtySixMonthStrategy>();
                x.For<IRepository>().Use<CsvRepository>().Ctor<string>("filePath").Is(path);
                x.For<ILoanMatcher>().Use<LoanMatcher>();
                x.For<IOutputDevice>().Use<OutputDevice>();
            });
        }
    }
}