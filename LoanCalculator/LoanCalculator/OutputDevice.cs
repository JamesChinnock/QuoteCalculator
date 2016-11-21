using System;

namespace LoanCalculator
{
    public class OutputDevice : IOutputDevice
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}