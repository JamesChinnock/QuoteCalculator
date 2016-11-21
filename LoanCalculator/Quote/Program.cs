using System;
using LoanCalculator;

namespace Quote
{
    public class Program
    {
        private const int Months = 36;

        static void Main(string[] args)
        {
            var path = args[0];
            var principal = decimal.Parse(args[1]);

            var quoteCalculator = GetCalculator(path);

            var quote = quoteCalculator.CalculateLoan(principal, Months);

            Console.WriteLine(string.Format("Requested Amount: £{0}", quote.RequestedAmount));
            Console.WriteLine(string.Format("Rate: {0}%", Math.Round(quote.Rate*100, 2)));
            Console.WriteLine(string.Format("Monthly Payment: £{0}", Math.Round(quote.MonthlyPayment, 2)));
            Console.WriteLine(string.Format("Total Repayment: £{0}", Math.Round(quote.TotalRepayment, 2)));

            Console.ReadLine();
        }

        private static ICalculator GetCalculator(string path)
        {
            var quoteRegistry = new QuoteRegistry();
            var container = quoteRegistry.GetContainer(path);
            return container.GetInstance<ICalculator>();
        }
    }
}
