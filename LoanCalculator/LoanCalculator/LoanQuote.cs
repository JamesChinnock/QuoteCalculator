namespace LoanCalculator
{
    public class LoanQuote
    {
        public decimal RequestedAmount { get; set; }
        public double Rate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal TotalRepayment { get; set; }
    }
}