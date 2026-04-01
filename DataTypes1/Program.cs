using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes1
{
    internal class Program
    {
        private const double InitialDeposit = 1000;
        private const int Years = 3;
        private const double InterestRate = 10;

        public static double[] CalculateCompoundInterest(double initial_deposit, int years, double interest_rate)
        {
            double[] amounts = new double[years];
            double currentAmount = initial_deposit;
            double rateMultiplier = 1 + interest_rate / 100;

            for (var i = 1; i <= years; i++)
            {
                currentAmount *= rateMultiplier;
                amounts[i] = currentAmount;
            }

            return amounts;
        }

        public static void PrintResults(double[] results)
        {
            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine($"Год {i + 1}: {results[i]:F2} руб.");
            }
        }

        static void Main(string[] args)
        {
            double[] results = CalculateCompoundInterest(InitialDeposit, Years, InterestRate);
            PrintResults(results);
        }
    }
}
