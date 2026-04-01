using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes2
{
    internal class Program
    {
        private static char[][] CreateDiamond(int N)
        {
            int middle = N / 2;
            char[][] diamond = new char[N][];

            for (var row = 0; row < N; row++)
            {
                int distanceFromCenter = Math.Abs(row - middle);
                int leftXPosition = distanceFromCenter;
                int rightXPosition = N - 1 - distanceFromCenter;

                diamond[row] = new char[N];

                for (var col = 0; col < N; col++)
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
            char[][] diamond = CreateDiamond(N);

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
            PrintDiamond(5);
        }
    }
}
