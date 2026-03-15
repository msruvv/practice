using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes1
{
    internal class Program
    {
        public static string CalculateCompoundInterest(double initial_deposit, int years, double interest_rate)
        {
            var result = new StringBuilder();
            double currentAmount = initial_deposit;
            double rateMultiplier = 1 + interest_rate / 100;

            for (var i = 1; i <= years; i++)
            {
                currentAmount *= rateMultiplier;
                string yearLine = $"Год {i}: {currentAmount:F2} руб.";
                result.AppendLine(yearLine);
            }

            return result.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine(CalculateCompoundInterest(1000, 3, 10));
        }
    }
}
