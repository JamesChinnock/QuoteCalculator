namespace LoanCalculator
{
    public interface ICompoundStrategy
    {
        decimal CompoundInterest(decimal principal, double interestRate);
    }
}