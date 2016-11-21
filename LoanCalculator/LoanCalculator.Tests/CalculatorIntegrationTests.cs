using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace LoanCalculator.Tests
{
    [TestClass]
    public class CalculatorIntegrationTests : TestsBase
    {
        private ICalculator _calculator;
        private ICompoundStrategy _compoundStrategy;
        private IOutputDevice _outputDevice;
        private ILoanMatcher _loanMatcher;

        private IRepository _repository;

        [TestInitialize]
        public void Init()
        {
            _compoundStrategy = MockRepository.GenerateMock<ICompoundStrategy>();
            _repository = MockRepository.GenerateMock<IRepository>();
            _outputDevice = MockRepository.GenerateMock<IOutputDevice>();
            _loanMatcher = MockRepository.GenerateMock<ILoanMatcher>();

            _calculator = new Calculator(_compoundStrategy, _repository, _outputDevice, _loanMatcher);

        }

        [TestMethod]
        public void InitializeNewInstance()
        {
            _calculator = new Calculator(_compoundStrategy, _repository, _outputDevice, _loanMatcher);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitializeNewInstanceNullCompoundStrategyThrowsException()
        {
            _calculator = new Calculator(null, _repository, _outputDevice, _loanMatcher);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitializeNewInstanceNullOutputDeviceThrowsException()
        {
            _calculator = new Calculator(_compoundStrategy, _repository, null, _loanMatcher);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitializeNewInstanceNullRepositoryThrowsException()
        {
            _calculator = new Calculator(_compoundStrategy, null, _outputDevice, _loanMatcher);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitializeNewInstanceNullLoanMatcherThrowsException()
        {
            _calculator = new Calculator(_compoundStrategy, _repository, _outputDevice, null);
        }

        [TestMethod]
        public void InvokingCalculateLoan_InvokesCompoundInterestOnStrategy()
        {
            _compoundStrategy.Expect(x => x.CompoundInterest(Arg<decimal>.Is.Anything, Arg<double>.Is.Anything)).Return(100);
            _repository.Expect(x => x.RetrieveAvailableLenders()).Return(LenderList);
            _loanMatcher.Expect(x => x.Match(Arg<decimal>.Is.Anything, Arg<List<Lender>>.Is.Anything)).Return(MatchedLenderList);

            _calculator.CalculateLoan(640, 36);
            _compoundStrategy.VerifyAllExpectations();
        }

        [TestMethod]
        public void InvokingCalculateLoan_InvokesRetrieveAvailableLendersOnRepository()
        {
            _compoundStrategy.Expect(x => x.CompoundInterest(Arg<decimal>.Is.Anything, Arg<double>.Is.Anything)).Return(100);
            _repository.Expect(x => x.RetrieveAvailableLenders()).Return(LenderList);
            _loanMatcher.Expect(x => x.Match(Arg<decimal>.Is.Anything, Arg<List<Lender>>.Is.Anything)).Return(MatchedLenderList);

            _calculator.CalculateLoan(640, 36);
            _repository.VerifyAllExpectations();
        }

        [TestMethod]
        public void InvokingCalculateLoan_InvokesMatchOnLoanMatcherOnRepository()
        {
            _compoundStrategy.Expect(x => x.CompoundInterest(Arg<decimal>.Is.Anything, Arg<double>.Is.Anything)).Return(100);
            _repository.Expect(x => x.RetrieveAvailableLenders()).Return(LenderList);
            _loanMatcher.Expect(x => x.Match(Arg<decimal>.Is.Anything, Arg<List<Lender>>.Is.Anything)).Return(MatchedLenderList);

            _calculator.CalculateLoan(640, 36);
            _loanMatcher.VerifyAllExpectations();
        }

    }
}
