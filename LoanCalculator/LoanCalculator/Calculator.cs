using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanCalculator
{
    public class Calculator : ICalculator
    {
        private readonly ICompoundStrategy _compoundStrategy;
        private readonly IRepository _repository;
        private readonly IOutputDevice _outputDevice;
        private readonly ILoanMatcher _matcher;

        public Calculator(ICompoundStrategy compoundStrategy, IRepository repository, IOutputDevice outputDevice, ILoanMatcher matcher)
        {
            if (compoundStrategy == null) throw new ArgumentNullException(nameof(compoundStrategy));
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (outputDevice == null) throw new ArgumentNullException(nameof(outputDevice));
            if (matcher == null) throw new ArgumentNullException(nameof(matcher));

            _compoundStrategy = compoundStrategy;
            _repository = repository;
            _outputDevice = outputDevice;
            _matcher = matcher;
        }

        public LoanQuote CalculateLoan(decimal principal, int months)
        {
            if (principal <= 0)
            {
                throw new ArgumentException("Principal amount must be positive");
            }

            if (months <= 0)
            {
                throw new ArgumentException("Monthly term of loan must be positive");
            }

            if (!EnoughFundsAvailable(principal))
            {
                _outputDevice.Write("Not enough funds are currently available to service this loan");
                return new LoanQuote();
            }

            var rate = GetRate(principal);
            var total = GetCompoundInterest(principal, rate);
            var monthly = total/months;


            var loanQuote = new LoanQuote
            {
                RequestedAmount = principal,
                TotalRepayment = total,
                MonthlyPayment = monthly,
                Rate = rate
            };

            return loanQuote;
        }

        private double GetRate(decimal principal)
        {
            var availableLenders = GetOrderedLendersLowestRateFirst();
            var lenders = _matcher.Match(principal, availableLenders);

            // Huge assumption on how to get the actual rate - used averae of those lenders used
            // and their offered rates

            _repository.Update(lenders);

            return lenders.Average(x => x.Rate);
        }

        private bool EnoughFundsAvailable(decimal principal)
        {
            return !(GetSumOfAllAvailable() < principal);
        }

        private decimal GetSumOfAllAvailable()
        {
            return GetAvailableLenders().Sum(x => x.Available);
        }

        private IEnumerable<Lender> GetAvailableLenders()
        {
           return _repository.RetrieveAvailableLenders();
        }

        private List<Lender> GetOrderedLendersLowestRateFirst()
        {
            return GetAvailableLenders().OrderBy(x => x.Rate).ToList();
            
        }

        private decimal GetCompoundInterest(decimal principal, double rate)
        {
            try
            {
                return _compoundStrategy.CompoundInterest(principal, rate);
            }
            catch (Exception)
            {
                throw new ApplicationException("An eror occured calculating compounded monthly interest");    
            }
        }
    }
}