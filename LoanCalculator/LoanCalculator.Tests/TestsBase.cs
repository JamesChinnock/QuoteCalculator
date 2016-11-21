using System.Collections.Generic;

namespace LoanCalculator.Tests
{
    public class TestsBase
    {
        public List<Lender> LenderList
        {
            get
            {
                return new List<Lender>
                {
                    new Lender {Name = "Bob", Rate = 0.075, Available = 640},
                    new Lender {Name = "Jane", Rate = 0.069, Available = 480},
                    new Lender {Name = "Fred", Rate = 0.071, Available = 520},
                    new Lender {Name = "Mary", Rate = 0.104, Available = 170},
                    new Lender {Name = "John", Rate = 0.081, Available = 320},
                    new Lender {Name = "Dave", Rate = 0.074, Available = 140},
                    new Lender {Name = "Angela", Rate = 0.071, Available = 60}

                };
            }
        }

        public List<Lender> MatchedLenderList
        {
            get
            {
                return new List<Lender>
                {
                    new Lender {Name = "Bob", Rate = 0.075, Available = 640}
                };
            }
        }
    }
}