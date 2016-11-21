using System.Collections.Generic;

namespace LoanCalculator
{
    public interface IRepository
    {
        List<Lender> RetrieveAvailableLenders();

        void Update(List<Lender> lenders);
    }
}