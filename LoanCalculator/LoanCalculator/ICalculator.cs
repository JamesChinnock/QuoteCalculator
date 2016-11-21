namespace LoanCalculator
{
    public interface ICalculator
    {
        LoanQuote CalculateLoan(decimal principal, int months);
    }
}