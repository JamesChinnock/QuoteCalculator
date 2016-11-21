using System;
using System.Collections.Generic;

namespace LoanCalculator
{
    public class LoanMatcher : ILoanMatcher
    {
        public List<Lender> Match(decimal principal, List<Lender> lenders)
        {
            if (principal <= 0)
            {
                throw new ArgumentException("The loan principal must be positive");
            }

            if (lenders == null || lenders.Count <= 0)
            {
                throw new ArgumentException("No available lenders were available in the matching algorithm");
            }

            decimal runningtotal = 0;
            var listOfMatches = new List<Lender>();

            foreach (var lender in lenders)
            {
                if (runningtotal == principal)
                {
                    break;
                }

                if ((runningtotal < principal) && (principal - runningtotal >= lender.Available))
                {
                    // Full match
                    listOfMatches.Add(lender);
                    runningtotal += lender.Available;
                    continue;
                }

                if (principal - runningtotal < lender.Available)
                {
                    // partial match
                    var used = principal - runningtotal;

                    var newLender = new Lender
                    {
                        Name = lender.Name,
                        Rate = lender.Rate,
                        Available = used
                    };

                    listOfMatches.Add(newLender);
                    break;
                }
            }

            return listOfMatches;
            
        }
    }
}