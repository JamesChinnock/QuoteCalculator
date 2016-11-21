using System.Collections.Generic;

namespace LoanCalculator
{
    public interface ILoanMatcher
    {
        List<Lender> Match(decimal principal, List<Lender> lenders);
    }
}