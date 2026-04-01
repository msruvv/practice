using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes2
{
    internal class Program
    {
        private const int DiamontSize = 5;

        private static char[][] CreateDiamond(int n)
        {
            int middle = n / 2;
            char[][] diamond = new char[n][];

            for (var row = 0; row < n; row++)
            {
                int distanceFromCenter = Math.Abs(row - middle);
                int leftXPosition = distanceFromCenter;
                int rightXPosition = n - 1 - distanceFromCenter;

                diamond[row] = new char[n];

                for (var col = 0; col < n; col++)
                {
                    diamond[row][col] =
                        col == leftXPosition || col == rightXPosition
                        ? 'X'
                        : ' ';
                }
            }

            return diamond;
        }

        public static void PrintDiamond(int N)
        {
            var diamond = CreateDiamond(N);

            for (var row = 0; row < diamond.Length; row++)
            {
                for (var col = 0; col < diamond[row].Length; col++)
                {
                    Console.Write(diamond[row][col]);
                }

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            PrintDiamond(DiamontSize);
        }
    }
}
