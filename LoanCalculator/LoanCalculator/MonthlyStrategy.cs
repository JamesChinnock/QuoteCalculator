using System;

namespace LoanCalculator
{
    public class ThirtySixMonthStrategy : ICompoundStrategy
    {
        private const int Month = 12;
        private const int Years = 3;

        public decimal CompoundInterest(decimal principal, double interestRate)
        {
            var body = CalculateFunctionBody(interestRate);
            var exponent = CalculateTimePeriods(Years);
            return principal * CalculatePower(body, exponent);
        }

        private decimal CalculatePower(double body, double exponent)
        {
            return Convert.ToDecimal(Math.Pow(body, exponent));
        }

        private static double CalculateFunctionBody(double interestRate)
        {
            return 1 + (interestRate / Month);
        }

        private static double CalculateTimePeriods(double years)
        {
            return Month * years;
        }
    }
}