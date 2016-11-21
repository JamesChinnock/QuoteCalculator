using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace LoanCalculator
{
    public class CsvRepository : IRepository
    {
        private readonly string _filePath;

        public CsvRepository(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            _filePath = filePath;
        }

        public List<Lender> RetrieveAvailableLenders()
        {
            return ParseCsvFile();
        }

        public void Update(List<Lender> lenders)
        {
            // would update repository by making lenders as matched and no longer in pool
        }

        private List<Lender> ParseCsvFile()
        {
            var listOfLenders = new List<Lender>();

            using (var parser = new TextFieldParser(_filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                var header = parser.ReadLine();
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();

                    if (fields == null || fields.Length != 3)
                    {
                        throw new ApplicationException("The csv file is possibly in an incorrect format");
                    }

                    var lender = new Lender
                    {
                        Name = fields[0],
                        Rate = TryConvertRate(fields[1]),
                        Available = TryConvertAvailable(fields[2])
                    };

                    listOfLenders.Add(lender);
               }
            }

            return listOfLenders;
        }

        private static double TryConvertRate(string rate)
        {
            double value;
            if (!double.TryParse(rate, out value))
            {
                throw new ApplicationException(string.Format("The rate value {0} cannot be converted to a double", rate));
            }

            return value;
        }

        private static decimal TryConvertAvailable(string available)
        {
            decimal value;
            if (!decimal.TryParse(available, out value))
            {
                throw new ApplicationException(string.Format("The available value {0} cannot be converted to a double", available));
            }

            return value;
        }
    }
}